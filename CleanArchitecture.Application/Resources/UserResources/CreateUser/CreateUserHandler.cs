using AutoMapper;
using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Application.Resources.Emails;

namespace CleanArchitecture.Application.Resources.UserResources.CreateUser
{
    public sealed class CreateUserHandler : IHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(IUserService userService, IPasswordService passwordService, IEmailService emailService, ITokenService tokenService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _passwordService = passwordService;
            _emailService = emailService;
            _tokenService = tokenService;

            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateUserResponse> HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            request.Validate();

            await _userService.EnsureEmailNotRegisteredAsync(request.Email, cancellationToken);

            await _userService.EnsureUsernameNotRegisteredAsync(request.Username, cancellationToken);

            await _passwordService.EnsureEntropyIsValid(request.Password);

            var newUser = await _userService.CreateUserAsync(request, cancellationToken);

            string token = _tokenService.GenerateTokenECDSA(newUser);

            var createEmailDto = EmailsTemplates.CreateEmailRegister(newUser.Email.Address, token);
            await _emailService.SendEmailAsync(createEmailDto);

            await _unitOfWork.SaveAsync(cancellationToken);

            var response = new CreateUserResponse(true, $"O usuário foi criado com sucesso. Enviamos um link de confirmação para o endereço de e-mail {newUser.Email.Address}.");

            return _mapper.Map(newUser, response);
        }
    }
}
