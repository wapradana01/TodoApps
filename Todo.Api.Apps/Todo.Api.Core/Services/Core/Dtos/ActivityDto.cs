using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Shared.Enums;

namespace Todo.Api.Core.Services.Core.Dtos
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public string ActivityNo { get; set; } = string.Empty;
        public string ActivityTitle { get; set; } = string.Empty;
        public string ActivityDesc { get; set; } = string.Empty;
        public ActivityStatus ActivityStatus { get; set; }
    }
}
