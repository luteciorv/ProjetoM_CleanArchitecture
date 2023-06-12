﻿using CleanArchitecture.Application.DTOs.User;
using CleanArchitecture.Application.Resources.UserResources.CreateUser;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using Profile = AutoMapper.Profile;

namespace CleanArchitecture.Application.Resources.UserResources
{
    public sealed class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, ReadUserDto>()
                .ForMember(dto => dto.Email, map => map.MapFrom(user => $"Email -- {user.Email.Address}"))
                .ForMember(dto => dto.EmailVerified, map => map.MapFrom(user => user.Email.Verified));

            CreateMap<User, CreateUserResponse>()
                .ForMember(response => response.Data, map =>
                {
                    map.MapFrom(user => new ReadUserDto
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email.Address,
                        EmailVerified = user.Email.Verified,
                        AccessFailedCount = user.AccessFailedCount,
                        IsActive = user.IsActive
                    });
                });
        }
    }
}