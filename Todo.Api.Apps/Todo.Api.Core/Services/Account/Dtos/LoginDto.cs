using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Api.Core.Services.Account.Dtos
{
    public class LoginDto : TokenDto
    {
        public string FullName { get; set; } = string.Empty;
    }
}
