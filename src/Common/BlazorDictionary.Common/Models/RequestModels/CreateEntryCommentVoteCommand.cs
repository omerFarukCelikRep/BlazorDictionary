using MediatR;

namespace BlazorDictionary.Common.Models.RequestModels;

public class CreateEntryCommentVoteCommand : IRequest<bool>
{
    public CreateEntryCommentVoteCommand()
    {

    }
    public CreateEntryCommentVoteCommand(Guid entryCommentId, VoteType voteType, Guid createdBy)
    {
        EntryCommentId = entryCommentId;
        VoteType = voteType;
        CreatedBy = createdBy;
    }

    public Guid EntryCommentId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedBy { get; set; }
}
