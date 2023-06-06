using AutoMapper;
using CleanArchitecture.Application.DTOs.User;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.Services;
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

        public async Task<bool> CheckEmailRegisteredAsync(string email, CancellationToken cancellationToken) =>
            await _unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken) is not null;

        public async Task<bool> CheckUsernameRegisteredAsync(string username, CancellationToken cancellationToken) =>
            await _unitOfWork.UserRepository.GetByUsernameAsync(username, cancellationToken) is not null;

        public async Task<byte[]> GeneratePasswordHashAsync(string password, string salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            return await _passwordService.CreateHashAsync(passwordBytes, saltBytes);
        }
    }
}