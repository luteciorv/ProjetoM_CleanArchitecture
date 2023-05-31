using AutoMapper;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Resources.UserResources.CreateUser
{
    public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper) =>
            (_unitOfWork, _userRepository, _mapper) = (unitOfWork, userRepository, mapper);

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {            
            var user = _mapper.Map<User>(request);
            
            _userRepository.Create(user);
            await _unitOfWork.SaveAsync(cancellationToken);

            var response = _mapper.Map<CreateUserResponse>(user);
            return response;
        }
    }
}
