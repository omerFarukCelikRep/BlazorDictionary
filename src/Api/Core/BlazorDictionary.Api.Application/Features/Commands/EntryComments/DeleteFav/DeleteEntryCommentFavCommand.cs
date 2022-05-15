using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComments.DeleteFav;

public class DeleteEntryCommentFavCommand : IRequest<bool>
{
    public DeleteEntryCommentFavCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }

    public Guid EntryCommentId { get; set; }
    public Guid UserId { get; set; }
}
