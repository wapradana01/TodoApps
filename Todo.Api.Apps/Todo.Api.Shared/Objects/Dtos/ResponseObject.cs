using Todo.Api.Shared.Enums;

namespace Todo.Api.Shared.Objects.Dtos
{
    public class ResponseObject<T>(string? message = null, ResponseCode responseCode = ResponseCode.BadRequest) : ResponseBase(message, responseCode)
        where T : class
    {
        public T? Obj { get; set; }
    }
}
