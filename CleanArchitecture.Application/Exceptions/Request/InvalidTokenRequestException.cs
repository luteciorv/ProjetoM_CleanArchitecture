using Flunt.Notifications;

namespace CleanArchitecture.Application.Exceptions.Request
{
    internal class InvalidTokenRequestException : InvalidRequestException
    {
        public InvalidTokenRequestException(string message) : base(message) { }
        public InvalidTokenRequestException(string message, List<Notification> errors) : base(message, errors) { }
    }
}
