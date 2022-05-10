namespace BlazorDictionary.Common.Events.EntryComments;

public class CreateEntryCommentFavEvent
{
    public Guid EntryCommentId { get; set; }
    public Guid CreatedBy { get; set; }
}