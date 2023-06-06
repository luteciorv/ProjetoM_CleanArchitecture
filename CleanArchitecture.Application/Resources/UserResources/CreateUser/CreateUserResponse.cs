using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.DTOs.User;
using Flunt.Notifications;

namespace CleanArchitecture.Application.Resources.UserResources.CreateUser
{
    public sealed class CreateUserResponse : BaseResponse
    {
        public CreateUserResponse(bool isSuccess, string message) : base(isSuccess, message)
        { }

        public CreateUserResponse(bool isSuccess, string message, List<Notification> errors) : base(isSuccess, message, errors)
        { }

        public ReadUserDto? Data { get; private set; }
    }
}
