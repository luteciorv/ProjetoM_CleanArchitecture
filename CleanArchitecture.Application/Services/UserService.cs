using AutoMapper;
using CleanArchitecture.Application.DTOs.User;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Application.Queries;

namespace CleanArchitecture.Application.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

        public async Task<IEnumerable<ReadUserDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync(cancellationToken);
            var readUsers = _mapper.Map<IEnumerable<ReadUserDto>>(users);
            return readUsers;
        }
        
        public async Task<bool> CheckEmailRegisteredAsync(string email, CancellationToken cancellationToken) =>
            await _unitOfWork.UserRepository.AnyAsync(UserQueries.GetByEmail(email), cancellationToken);
    }
}