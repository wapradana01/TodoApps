using Todo.Api.Shared.Objects.Inputs.Interfaces;

namespace Todo.Api.Shared.Objects.Inputs
{
    public class PagingInputBase : IPagingInput
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
