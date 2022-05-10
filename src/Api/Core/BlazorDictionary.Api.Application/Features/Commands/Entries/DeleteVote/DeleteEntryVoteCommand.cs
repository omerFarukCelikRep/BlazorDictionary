using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Entries.DeleteVote;

public class DeleteEntryVoteCommand : IRequest<bool>
{
    public DeleteEntryVoteCommand(Guid entryId, Guid userId)
    {
        EntryId = entryId;
        UserId = userId;
    }

    public Guid EntryId { get; set; }
    public Guid UserId { get; set; }
}
