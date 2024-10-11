namespace Todo.Api.Shared.Objects
{
    public class CurrentUserAccessor
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public IEnumerable<string>? Permissions { get; set; }
    }
}
