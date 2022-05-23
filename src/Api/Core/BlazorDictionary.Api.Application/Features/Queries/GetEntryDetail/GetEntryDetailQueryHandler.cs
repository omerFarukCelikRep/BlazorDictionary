using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Api.Application.Features.Queries.GetEntryDetail;
public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
{
    private readonly IEntryRepository _entryRepository;
    private readonly IMapper _mapper;

    public GetEntryDetailQueryHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        _entryRepository = entryRepository;
        _mapper = mapper;
    }
    public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        query = query.Include(x => x.EntryFavorites)
                     .Include(x => x.CreatedBy)
                     .Include(x => x.EntryVotes)
                     .Where(x => x.Id == request.EntryId);

        var list = query.Select(x => new GetEntryDetailViewModel
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

        return await list.FirstOrDefaultAsync(cancellationToken);
    }
}
