using Todo.Api.Shared.Enums;

namespace Todo.Api.Shared.Objects.Exceptions
{
    public class CustomValidationException(ResponseCode code, string message) : Exception(message)
    {
        public ResponseCode Code { get; } = code;
    }
}
