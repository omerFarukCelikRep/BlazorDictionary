namespace BlazorDictionary.Common.Events.EntryComments;

public class DeleteEntryCommentVoteEvent
{
    public Guid EntryCommentId { get; set; }
    public Guid CreatedBy { get; set; }
}
