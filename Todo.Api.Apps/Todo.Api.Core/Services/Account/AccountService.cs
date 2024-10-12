using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Core.Services.Account.Dtos;
using Todo.Api.DataAccess.Models;
using Todo.Api.DataAccess;
using Todo.Api.Shared.Enums;
using Todo.Api.Shared.Objects.Dtos;
using Todo.Api.Core.Services.Account.Inputs;
using System.Data;

namespace Todo.Api.Core.Services.Account
{
    public class AccountService(ApplicationDbContext dbContext, IMapper mapper, ILogger<AccountService> logger, TokenService tokenService)
        : BaseService<AccountService>(dbContext, mapper, logger)
    {
        private readonly TokenService _tokenService = tokenService;

        public async Task<ResponseObject<LoginDto>> Login(LoginInput input)
        {
            var dataUser = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(d => d.UserName.Equals(input.Username) && d.Password.Equals(input.Password));

            if (dataUser == null)
            {
                return new ResponseObject<LoginDto>($"User Name dan Password yang anda masukkan salah", ResponseCode.UnAuthorized);
            }

            var token = await GenerateAccessToken(dataUser);

            return new ResponseObject<LoginDto>(responseCode: ResponseCode.Ok)
            {
                Obj = new LoginDto
                {
                    FullName = dataUser.FullName,
                    AccessToken = token.AccessToken,
                    ExpiredAt = token.ExpiredAt,
                    RefreshToken = token.RefreshToken,
                    SessionExpiredAt = token.SessionExpiredAt
                }
            };
        }

        public async Task<ResponseObject<TokenDto>> RefreshToken(Guid userId)
        {
            var dataUser = await _dbContext.Users.FirstOrDefaultAsync(d => d.Id.Equals(userId));

            if (dataUser == null)
                return new ResponseObject<TokenDto>($"Gagal memperbaharui akses token", ResponseCode.Forbidden);

            return new ResponseObject<TokenDto>(responseCode: ResponseCode.Ok)
            {
                Obj = await GenerateAccessToken(dataUser)
            };
        }

        public async Task<ResponseObject<UserDto>> AddNewUser(UserInput input)
        {
            var existUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == input.UserName);

            if (existUser != null)
            {
                return new ResponseObject<UserDto>($"User telah ada, silahkan coba login atau gunakan user name yang berbeda", ResponseCode.BadRequest);
            }

            var newUser = new User
            {
                UserName = input.UserName,
                FullName = input.FullName,
                Password = input.Password
            };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return new ResponseObject<UserDto>(responseCode: ResponseCode.Ok)
            {
                Obj = new UserDto
                {
                    UserName = input.UserName,
                    FullName = input.FullName
                }
            };
        }

        private async Task<TokenDto> GenerateAccessToken(User dataUser)
        {
            var dataPermissions = new List<string> { "Administrator" };

            return await _tokenService.GenerateToken(dataUser.UserName, dataUser.Id, dataUser.FullName, dataPermissions);
        }
    }
}
