using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Core.Services.Core.Dtos;
using Todo.Api.Core.Services.Core.Inputs;
using Todo.Api.Core.Services.Master;
using Todo.Api.DataAccess;
using Todo.Api.DataAccess.Models;
using Todo.Api.Shared.Enums;
using Todo.Api.Shared.Objects;
using Todo.Api.Shared.Objects.Dtos;
using Todo.Api.Shared.Objects.Inputs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Todo.Api.Core.Services.Core
{
    public class ActivityService(ApplicationDbContext dbContext, IMapper mapper, ILogger<ActivityService> logger, DocumentNumberService documentNumberService)
        : BaseService<ActivityService>(dbContext, mapper, logger)
    {
        private readonly DocumentNumberService _documentNumberService = documentNumberService;

        public async Task<ResponseObject<ActivityDto>> CreateActivity(List<ActivityInput> input)
        {
            foreach (var item in input)
            {
                var runningNumber = await _documentNumberService.GetRunningNumberDocument("AC");
                var newActivity = new TodoActivity
                {
                    ActivityTitle = item.ActivityTitle,
                    ActivityDesc = item.ActivityDesc,
                    ActivityNo = $"AC-{runningNumber.RunningNumber.ToString().PadLeft(4, '0')}",
                    ActivityStatus = ActivityStatus.Unmarked
                };

                await _dbContext.TodoActivities.AddAsync(newActivity);
            }

            await _dbContext.SaveChangesAsync();
            
            return new ResponseObject<ActivityDto>(responseCode: ResponseCode.Ok);
        }

        public async Task<ResponsePaging<ActivityDto>> GetListActivity(PagingSearchInputBase input, CurrentUserAccessor currentUserAccessor)
        {
            var retVal = new ResponsePaging<ActivityDto>();

            var data = _dbContext.TodoActivities
                .Where(x => x.CreatedBy == currentUserAccessor.Id.ToString())
                .Where(x => 
                    EF.Functions.Like(x.ActivityTitle, $"%{input.SearchKey}%")
                    || EF.Functions.Like(x.ActivityDesc, $"%{input.SearchKey}%"))
                .OrderByDescending(x => x.Modified ?? x.Created);

            var dataActivityPaging = new ResponsePaging<TodoActivity>();
            dataActivityPaging.ApplyPagination(input.Page, input.PageSize, data);

            var retValData = dataActivityPaging.Data?.ToList()
                                                             .AsQueryable()
                                                             .Select(d =>
                                                                _mapper.Map<ActivityDto>(d));

            retVal.CopyPaginationInfo(dataActivityPaging, retValData);

            return await Task.FromResult(retVal);
        }

        public async Task<ResponseObject<object>> DeleteActivity(Guid id)
        {
            var retVal = new ResponseObject<object>();

            var data = await _dbContext.TodoActivities.FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
            {
                data.RowStatus = 1;

                _dbContext.TodoActivities.Update(data);

                await _dbContext.SaveChangesAsync();
            }

            return new ResponseObject<object>(responseCode: ResponseCode.Ok);
        }

        public async Task<ResponseObject<ActivityDto>> UpdataStatusActivity(UpdateActivityStatusInput input)
        {
            var retVal = new ResponseObject<ActivityDto>(responseCode : ResponseCode.Ok);

            var data = await _dbContext.TodoActivities.FirstOrDefaultAsync(x => x.Id == input.Id && x.ActivityStatus == ActivityStatus.Unmarked);

            if (data != null)
            {
                data.ActivityStatus = input.Status;

                _dbContext.TodoActivities.Update(data);

                await _dbContext.SaveChangesAsync();

                retVal.Obj = _mapper.Map<ActivityDto>(data);
            }

            return retVal;
        }

        public async Task<ResponseObject<ActivityDto>> UpdataDataActvity(ActivityInput input)
        {
            var retVal = new ResponseObject<ActivityDto>(responseCode: ResponseCode.Ok);

            if (input.Id.HasValue)
            {
                var data = await _dbContext.TodoActivities.FirstOrDefaultAsync(x => x.Id == input.Id);

                if (data != null)
                {
                    data.ActivityTitle = input.ActivityTitle;
                    data.ActivityDesc = input.ActivityDesc;

                    _dbContext.TodoActivities.Update(data);

                    await _dbContext.SaveChangesAsync();

                    retVal.Obj = _mapper.Map<ActivityDto>(data);
                }
            }

            return retVal;
        }
    }
}
