using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Infrastracture.Extensions;
using BlazorDictionary.Common.Models.Pages;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Api.Application.Features.Queries.GetEntryComments;
public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
{
    private readonly IEntryCommentRepository _entryCommentRepository;

    public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
    {
        _entryCommentRepository = entryCommentRepository;
    }
    public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = _entryCommentRepository.AsQueryable();

        query = query.Include(x => x.EntryCommentFavorites)
                     .Include(x => x.CreatedBy)
                     .Include(x => x.EntryCommentVotes)
                     .Where(x => x.EntryId == request.EntryId);

        var list = query.Select(x => new GetEntryCommentsViewModel
        {
            Id = x.Id,
            Content = x.Content,
            IsFavorited = request.UserId.HasValue && x.EntryCommentFavorites.Any(x => x.CreatedById == request.UserId),
            FavoritedCount = x.EntryCommentFavorites.Count,
            CreatedDate = x.CreateDate,
            CreatedByUserName = x.CreatedBy.Username,
            VoteType = request.UserId.HasValue && x.EntryCommentVotes.Any(x => x.CreatedById == request.UserId)
                            ? x.EntryCommentVotes.FirstOrDefault(x => x.CreatedById == request.UserId).VoteType
                            : Common.Models.VoteType.None
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}
