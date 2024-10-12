﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Api.Shared.Attributes;
using Todo.Api.Shared.Constants;
using Todo.Api.Shared.Enums;
using Todo.Api.Shared.Objects.Dtos;
using Todo.Api.Shared.Objects;

namespace Todo.Api.Apps.Middlewares
{
    public class AuthorizationFilter(ILogger<AuthorizationFilter> logger, CurrentUserAccessor currentUserAccessor) : IAuthorizationFilter
    {
        private readonly ILogger<AuthorizationFilter> _logger = logger;
        private readonly string logPrefix = nameof(AuthorizationFilter);
        protected CurrentUserAccessor _currentUserAccessor = currentUserAccessor;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var response = new ResponseBase("Authorization has been denied for this request.", ResponseCode.UnAuthorized);

            if (SkipAuthorization(context))
            {
                _logger.LogInformation("{Prefix}: Allow anonymous access", logPrefix);
                return;
            }

            var bearerToken = context.HttpContext.Request.Headers.Authorization;
            _logger.LogInformation("{Prefix}: {BearerToken}", logPrefix, bearerToken);

            try
            {
                if (context.HttpContext.User.Identity is ClaimsIdentity identity && identity.Claims != null && identity.Claims.Any())
                {
                    var permissions = identity.Claims.Where(i => i.Type == PermissionConstants.TypeCode);

                    if (IsAuthorize(context, permissions))
                    {
                        var sub = identity.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
                        var sid = identity.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))?.Value;
                        var username = identity.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid"))?.Value;

                        _currentUserAccessor.Id = new Guid(sub ?? "00000000-0000-0000-0000-000000000000");
                        _currentUserAccessor.FullName = sid ?? _currentUserAccessor.FullName;
                        _currentUserAccessor.UserName = username ?? _currentUserAccessor.UserName;
                        _currentUserAccessor.Permissions = permissions.Select(p => p.Value);

                        return;
                    }
                }

                _logger.LogInformation("{Prefix}: User does not have required permissions.", logPrefix);
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Prefix}: Error occurred while authorizing the request: {Message}", logPrefix, ex.Message);
                context.HttpContext.Response.StatusCode = 401;
                response.UnAuthorized(ex.Message);
                context.Result = new JsonResult(response);
                return;
            }
        }

        private static bool SkipAuthorization(AuthorizationFilterContext context) => context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        private bool IsAuthorize(AuthorizationFilterContext context, IEnumerable<Claim> claims)
        {
            // If user doesn't have any permission then the user is not authorize
            if (!claims.Any()) return false;

            // If user have super permission then the user is authorize
            if (claims.Any(c => c.Value.Equals(PermissionConstants.SuperPermission))) return true;

            var appAuthorizeAttributes = context.ActionDescriptor.EndpointMetadata.OfType<AppAuthorizeAttribute>();

            foreach (var appAuthorizeAttribute in appAuthorizeAttributes)
            {
                if (claims.Any(c => appAuthorizeAttribute.Permissions.Contains(c.Value)))
                {
                    _logger.LogInformation("{Prefix}: User is authorized based on specified permissions.", logPrefix);
                    return true;
                }
            }

            return false;
        }
    }
}
