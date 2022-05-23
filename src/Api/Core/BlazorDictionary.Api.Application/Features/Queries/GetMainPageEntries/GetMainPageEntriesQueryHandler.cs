using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Infrastracture.Extensions;
using BlazorDictionary.Common.Models.Pages;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Api.Application.Features.Queries.GetMainPageEntries;

public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }
    public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        query.Include(x => x.EntryFavorites)
             .Include(x => x.CreatedBy)
             .Include(x => x.EntryVotes);

        var entryDetails = query.Select(x => new GetEntryDetailViewModel
        {
            Id = x.Id,
            Subject = x.Subject,
            Content = x.Content,
            IsFavorited = request.UserId.HasValue && x.EntryFavorites.Any(x => x.CreatedById == request.UserId),
            FavoritedCount = x.EntryFavorites.Count,
            CreatedDate = x.CreateDate,
            CreatedByUserName = x.CreatedBy.Username,
            VoteType = request.UserId.HasValue && x.EntryVotes.Any(x => x.CreatedById == request.UserId)
                                    ? x.EntryVotes.FirstOrDefault(x => x.CreatedById == request.UserId).VoteType
                                    : Common.Models.VoteType.None
        });

        var entries = await entryDetails.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}
