using System.ComponentModel.DataAnnotations;

namespace Todo.Ui.Apps.Dtos
{
    public class ActivityDto : Tracking
    {
        [Key]
        public Guid Id { get; set; }
        public string ActivityNo { get; set; } = string.Empty;
        public string ActivityTitle { get; set; } = string.Empty;
        public string ActivityDesc { get; set; } = string.Empty;
        public int Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }

    public enum ActivityStatus
    {
        Done,
        Canceled,
        Unmarked
    }
}
