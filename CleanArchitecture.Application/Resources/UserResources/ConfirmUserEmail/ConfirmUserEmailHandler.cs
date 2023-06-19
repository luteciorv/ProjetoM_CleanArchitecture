using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Domain.Statics;

namespace CleanArchitecture.Application.Resources.UserResources.ConfirmUserEmail
{
    public sealed class ConfirmUserEmailHandler : IHandler<ConfirmUserEmailRequest, ConfirmUserEmailResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmUserEmailHandler(ITokenService tokenService, IUserService userService, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ConfirmUserEmailResponse> HandleAsync(ConfirmUserEmailRequest request, CancellationToken cancellationToken)
        {
            _tokenService.EnsureHaveClaim(request.Token, UserClaims.EMAIL);
            var email = _tokenService.GetClaimValue(request.Token, UserClaims.EMAIL);

            await _userService.ConfirmEmailAsync(email, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            
            return new ConfirmUserEmailResponse(true, $"O e-mail {email} foi confirmado com sucesso.");
        }
    }
}
