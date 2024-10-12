using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Shared.Enums;

namespace Todo.Api.Core.Services.Core.Inputs
{
    public class ActivityInput
    {
        public Guid? Id { get; set; }
        public string ActivityTitle { get; set; } = string.Empty;
        public string ActivityDesc { get; set; } = string.Empty;
    }
}
