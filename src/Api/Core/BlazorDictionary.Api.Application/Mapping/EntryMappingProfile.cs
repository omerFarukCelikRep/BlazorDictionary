using AutoMapper;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Common.Models.Queries;
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

        CreateMap<Entry, GetEntriesViewModel>()
            .ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count));
    }
}
