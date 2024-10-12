using System.ComponentModel.DataAnnotations;

namespace Todo.Ui.Apps.Dtos
{
    public class UserDto
    {
        [Required]
        [StringLength(16, ErrorMessage = "Identifier too long (16 character limit).")]
        public string? UserId { get; set; }

        public string? Password { get; set; }
    }

    public class UserRegistrationDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<ActivityDto> ToDoItems { get; set; } = new List<ActivityDto>();
    }

    public class UserLoginDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UserLoginResponse
    {
        public UserLoginData Data { get; set; } = new();
    }

    public class UserLoginData
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; 
        public DateTime LoginTime { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class Tracking
    {
        public DateTime? CreatedDate { get; set; }
        //public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        //public string ModifiedBy { get; set; }
        public int RowStatus { get; set; }
    }
}
