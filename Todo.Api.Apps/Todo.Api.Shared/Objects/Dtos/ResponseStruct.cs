using Todo.Api.Shared.Enums;

namespace Todo.Api.Shared.Objects.Dtos
{
    public class ResponseStruct<T>(string? message = null, ResponseCode responseCode = ResponseCode.BadRequest) : ResponseBase(message, responseCode)
        where T : struct
    {
        public T? Obj { get; set; }
    }
}
