using AutoMapper;
using CleanArchitecture.Application.DTOs.User;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Resources.UserResources.CreateUser
{
    public sealed class CreateUserMapper : Profile
    {
        public CreateUserMapper()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserResponse>()
                .ForMember(response => response.ReadUserDto, dto =>
                {
                    dto.MapFrom(user => new ReadUserDto(user.Id, user.Email));
                });
        }
    }
}
