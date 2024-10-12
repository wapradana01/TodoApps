using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Core.Services.Account;
using Todo.Api.Core.Services.Account.Dtos;
using Todo.Api.Core.Services.Account.Inputs;
using Todo.Api.Shared.Attributes;
using Todo.Api.Shared.Constants;
using Todo.Api.Shared.Enums;
using Todo.Api.Shared.Objects;
using Todo.Api.Shared.Objects.Dtos;

namespace Todo.Api.Apps.Controllers
{
    [Route("api/account")]
    public class AccountController(AccountService accountService, CurrentUserAccessor currentUserAccessor) : BaseController
    {
        private readonly CurrentUserAccessor _currentUserAccessor = currentUserAccessor;
        private readonly AccountService _accountService = accountService;

        [AllowAnonymous]
        [HttpPost("login")]
        [Mutation]
        public async Task<ResponseObject<LoginDto>> Login([FromBody] LoginInput input)
        {
            return await _accountService.Login(input);
        }

        [HttpPost("refreshtoken")]
        [AppAuthorize([PermissionConstants.SuperPermission])]
        public async Task<ResponseObject<TokenDto>> RefreshToken()
        {
            return await _accountService.RefreshToken(_currentUserAccessor.Id);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [Mutation]
        public async Task<ResponseObject<UserDto>> RegisterUser([FromBody] UserInput input)
        {
            return await _accountService.AddNewUser(input);
        }
    }
}
