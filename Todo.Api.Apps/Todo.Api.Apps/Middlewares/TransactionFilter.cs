﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Shared.Attributes;
using Todo.Api.Shared.Enums;
using Todo.Api.Shared.Objects.Dtos;

namespace Todo.Api.Apps.Middlewares
{
    public class TransactionFilter<TApplicationDbContext>(TApplicationDbContext dbContext, ILogger<TransactionFilter<TApplicationDbContext>> logger) : IAsyncActionFilter
        where TApplicationDbContext : DbContext
    {
        private readonly TApplicationDbContext _dbContext = dbContext;
        private readonly ILogger<TransactionFilter<TApplicationDbContext>> _logger = logger;
        private readonly string logPrefix = nameof(TransactionFilter<TApplicationDbContext>);

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Check if the method has the MutationAttribute
            bool isMutation = context.ActionDescriptor.EndpointMetadata.OfType<MutationAttribute>().Any();
            bool isCustomResponse = context.ActionDescriptor.EndpointMetadata.OfType<CustomResponseAttribute>().Any();

            _logger.LogInformation("{Prefix}: Open transaction scope", logPrefix);
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var resultContext = await next();

                if (resultContext.Result != null && resultContext.Exception == null)
                {
                    if (resultContext.Result is ObjectResult objectResult)
                    {
                        if (objectResult.Value is not ResponseBase && !isCustomResponse)
                        {
                            string errorMessage = "Use ResponseBase or it's inheritance";

                            resultContext.Result = new JsonResult(new ResponseBase(errorMessage, ResponseCode.Error));
                            throw new InvalidOperationException(errorMessage);
                        }

                        if (isMutation && objectResult.Value != null)
                        {
                            if ((objectResult.Value is ResponseBase responseBase && responseBase.Succeeded) || isCustomResponse)
                            {
                                _logger.LogInformation("{Prefix}: Commit transaction scope", logPrefix);
                                await transaction.CommitAsync();
                            }
                            else
                            {
                                _logger.LogInformation("{Prefix}: Rollback transaction scope: Status code 400", logPrefix);
                                await transaction.RollbackAsync();
                            }
                        }
                        else
                        {
                            _logger.LogInformation("{Prefix}: Rollback transaction scope: Because isn't use mutation", logPrefix);
                            await transaction.RollbackAsync();
                        }
                    }
                    else
                    {
                        _logger.LogInformation("{Prefix}: Rollback transaction scope: Because the result isn't ObjectResult", logPrefix);
                        await transaction.RollbackAsync();
                    }
                }
                else
                {
                    _logger.LogInformation("{logPrefix}: Rollback transaction scope: There is no result", logPrefix);
                    await transaction.RollbackAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{Prefix}: Rollback transaction scope: {Message}", logPrefix, ex.Message);
                await transaction.RollbackAsync();
            }
        }
    }
}
