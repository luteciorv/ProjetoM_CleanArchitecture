using Flunt.Notifications;

namespace CleanArchitecture.Application.Commands
{
    public abstract class BaseResponse
    {
        protected BaseResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        protected BaseResponse(bool isSuccess, string message, List<Notification> errors) : this(isSuccess, message) =>
            Errors = errors;

        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public IEnumerable<Notification> Errors { get; private set; } = Enumerable.Empty<Notification>();
    }
}
