using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.DataAccess.Bases;

namespace Todo.Api.DataAccess.Models
{
    public class DocNumberConfig : EntityBase
    {
        public string TransactionTypeCode { get; set; } = string.Empty;
        public string Description { get; set; } = String.Empty;
        public int RunningNumber { get; set; }
    }
}
