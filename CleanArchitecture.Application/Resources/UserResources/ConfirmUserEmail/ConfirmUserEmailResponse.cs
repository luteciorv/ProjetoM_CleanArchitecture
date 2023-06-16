using CleanArchitecture.Application.Commands;

namespace CleanArchitecture.Application.Resources.UserResources.ConfirmUserEmail
{
    public sealed class ConfirmUserEmailResponse : BaseResponse
    {
        public ConfirmUserEmailResponse(bool isSuccess, string message) : base(isSuccess, message) 
        { }
    }
}
