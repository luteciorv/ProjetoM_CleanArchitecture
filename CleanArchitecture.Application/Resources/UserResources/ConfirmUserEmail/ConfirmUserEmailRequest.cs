using CleanArchitecture.Application.Commands;

namespace CleanArchitecture.Application.Resources.UserResources.ConfirmUserEmail
{
    public sealed class ConfirmUserEmailRequest : BaseRequest
    {
        public string Token { get; set; } = string.Empty;

        public override void Validate() { }
    }
}
