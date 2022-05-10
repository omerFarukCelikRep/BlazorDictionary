namespace BlazorDictionary.Common.Events.EntryComments;

public class DeleteEntryCommentFavEvent
{
    public Guid EntryCommentId { get; set; }
    public Guid CreatedBy { get; set; }
}
