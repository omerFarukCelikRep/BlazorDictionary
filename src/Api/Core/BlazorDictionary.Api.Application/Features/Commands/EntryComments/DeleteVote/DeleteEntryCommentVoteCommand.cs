using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.EntryComments.DeleteVote;

public class DeleteEntryCommentVoteCommand : IRequest<bool>
{
    public DeleteEntryCommentVoteCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }

    public Guid EntryCommentId { get; set; }
    public Guid UserId { get; set; }
}