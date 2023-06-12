using Flunt.Notifications;

namespace CleanArchitecture.Application.Exceptions.Request
{
    public abstract class InvalidRequestException : BaseException
    {
        public InvalidRequestException(string message) : base(message) { }
        public InvalidRequestException(string message, List<Notification> errors) : base(message, errors) { }
    }
}
