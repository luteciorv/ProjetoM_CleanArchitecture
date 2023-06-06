using AutoMapper;
using CleanArchitecture.Application.Commands;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Domain.Entities;
using System.Text;

namespace CleanArchitecture.Application.Resources.UserResources.CreateUser
{
    public sealed class CreateUserHandler : IHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new CreateUserResponse(false, "Não foi possível efetuar a criação do usuário, pois a requisição está inválida", request.Notifications.ToList());

            bool emailAlreadyRegistered = await _userService.CheckEmailRegisteredAsync(request.Email, cancellationToken);
            if (emailAlreadyRegistered)
                return new CreateUserResponse(false, $"O e-mail {request.Email} já foi cadastrado no banco de dados.");

            bool usernameAlreadyRegistered = await _userService.CheckUsernameRegisteredAsync(request.Username, cancellationToken);
            if (usernameAlreadyRegistered)
                return new CreateUserResponse(false, $"O username {request.Username} já foi cadastrado no banco de dados.");

            var passwordEntropy = await _passwordService.CalculateEntropy(request.Password);
            if(!passwordEntropy.IsValid)
                return new CreateUserResponse(false, $"A senha informada é de caráter {passwordEntropy.SecurityLevel} ({passwordEntropy.EntropyValue} na escala de entropia). Considere aumentar sua complexidade.");

            request.Password = Convert.ToBase64String(await _userService.GeneratePasswordHashAsync(request.Password));
            var newUser = _mapper.Map<User>(request);

            await _unitOfWork.UserRepository.CreateAsync(newUser, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            var response = new CreateUserResponse(true, "Usuário criado com sucesso.");
            return _mapper.Map(newUser, response);
        }
    }
}
