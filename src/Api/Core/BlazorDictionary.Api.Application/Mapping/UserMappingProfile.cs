using AutoMapper;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Common.Models.Queries;

namespace BlazorDictionary.Api.Application.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, LoginUserViewModel>()
            .ReverseMap();
    }
}
