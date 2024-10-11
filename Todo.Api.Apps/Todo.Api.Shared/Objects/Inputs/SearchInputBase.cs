using Todo.Api.Shared.Objects.Inputs.Interfaces;

namespace Todo.Api.Shared.Objects.Inputs
{
    public class SearchInputBase : ISearchInput
    {
        public string? SearchKey { get; set; }
    }
}
