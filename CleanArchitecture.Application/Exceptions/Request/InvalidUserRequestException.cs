using Flunt.Notifications;

namespace CleanArchitecture.Application.Exceptions.Request
{
    public class InvalidUserRequestException : InvalidRequestException
    {
        public InvalidUserRequestException(string message) : base(message) { }

        public InvalidUserRequestException(string message, List<Notification> errors) : base(message, errors) { }
    }
}
