using AutoMapper;
using CleanArchitecture.Application.DTOs.User;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Exceptions.Email;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Application.Resources.UserResources.CreateUser;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using System.Text;

namespace CleanArchitecture.Application.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IPasswordService passwordService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _passwordService = passwordService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadUserDto>> GetAllActiveAsync(CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetAllActiveAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ReadUserDto>>(users);
        }

        public async Task EnsureEmailNotRegisteredAsync(string email, CancellationToken cancellationToken)
        {
            bool emailAlreadyRegistered = await _unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken) is not null;
            if (emailAlreadyRegistered)
                throw new EmailAlreadyRegisteredException($"O e-mail {email} já foi cadastrado no banco de dados.");
        }
            

        public async Task EnsureUsernameNotRegisteredAsync(string username, CancellationToken cancellationToken)
        {
            bool usernameAlreadyRegistered = await _unitOfWork.UserRepository.GetByUsernameAsync(username, cancellationToken) is not null;
            if(usernameAlreadyRegistered)
                throw new UsernameAlreadyRegisteredException($"O nome de usuário {username} já foi cadastrado no banco de dados.");
        }
            

        public async Task<Password> CreatePasswordAsync(string password)
        {
            var saltBytes = await _passwordService.GenerateSaltAsync();
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            var saltHash = Convert.ToBase64String(saltBytes);
            var passwordHash = Convert.ToBase64String(await _passwordService.CreateHashAsync(passwordBytes, saltBytes));

            return new Password(passwordHash, saltHash);
        }

        public async Task<User> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var password = await CreatePasswordAsync(request.Password);
            var email = new Email(request.Email);
            var newUser = new User(request.Username, email, password);

            await _unitOfWork.UserRepository.CreateAsync(newUser, cancellationToken);
            return newUser;
        }

        public async Task ConfirmEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken) ?? 
                throw new EmailNotRegisteredException("O e-mail informado não foi registrado no sistema.");
            
            if (user.Email.Verified)
                throw new EmailAlreadyConfirmedException("O e-mail informado já foi confirmado.");

            user.Email.ConfirmEmail();
            _unitOfWork.UserRepository.Update(user);
        }
    }
}