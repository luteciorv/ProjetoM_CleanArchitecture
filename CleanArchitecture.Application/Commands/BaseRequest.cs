using Flunt.Notifications;

namespace CleanArchitecture.Application.Commands
{
    public abstract class BaseRequest : Notifiable<Notification>
    {
        public abstract void Validate();
    }
}
