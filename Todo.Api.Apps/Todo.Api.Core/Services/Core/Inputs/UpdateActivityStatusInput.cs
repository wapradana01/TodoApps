using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Shared.Enums;

namespace Todo.Api.Core.Services.Core.Inputs
{
    public class UpdateActivityStatusInput
    {
        public Guid Id { get; set; }
        public ActivityStatus Status { get; set; }
    }
}
