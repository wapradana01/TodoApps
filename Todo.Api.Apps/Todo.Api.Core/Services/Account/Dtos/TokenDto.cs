﻿namespace Todo.Api.Core.Services.Account.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiredAt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime SessionExpiredAt { get; set; }
    }
}
