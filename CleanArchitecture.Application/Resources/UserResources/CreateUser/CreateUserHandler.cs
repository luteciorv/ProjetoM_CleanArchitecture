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

        public async Task<CreateUserResponse> HandlerAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new CreateUserResponse(false, "Não foi possível validar a requisição do usuário");

            var user = _mapper.Map<User>(request);
            
            await _userRepository.Create(user);
            await _unitOfWork.SaveAsync(cancellationToken);

            var response = _mapper.Map<CreateUserResponse>(user);
            return response;
        }
    }
}
