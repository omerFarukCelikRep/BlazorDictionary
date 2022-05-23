using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Infrastracture.Extensions;
using BlazorDictionary.Common.Models.Pages;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Api.Application.Features.Queries.GetUserEntries;
public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }
    public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        query = query.Include(x => x.EntryFavorites)
                     .Include(x => x.CreatedBy);

        if (request.UserId.HasValue && request.UserId != Guid.Empty)
        {
            query = query.Where(x => x.CreatedById == request.UserId);
        }
        else if (!string.IsNullOrEmpty(request.UserName))
        {
            query = query.Where(x => x.CreatedBy.Username == request.UserName);
        }
        else
        {
            return null;
        }

        var list = query.Select(x => new GetUserEntriesDetailViewModel
        {
            Id = x.Id,
            Subject = x.Subject,
            Content = x.Content,
            IsFavorited = false,
            FavoritedCount = x.EntryFavorites.Count,
            CreatedDate = x.CreateDate,
            CreatedByUserName = x.CreatedBy.Username
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}
