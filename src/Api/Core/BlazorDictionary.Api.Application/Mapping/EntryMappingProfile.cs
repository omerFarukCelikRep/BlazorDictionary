using AutoMapper;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Common.Models.RequestModels;

namespace BlazorDictionary.Api.Application.Mapping;

public class EntryMappingProfile : Profile
{
    public EntryMappingProfile()
    {
        CreateMap<Entry, CreateEntryCommand>()
            .ReverseMap();

        CreateMap<EntryComment, CreateEntryCommentCommand>()
            .ReverseMap();
    }
}
