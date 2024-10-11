using Todo.Api.DataAccess.Bases;

namespace Todo.Api.DataAccess.Models
{
    public partial class User : EntityBase
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}
