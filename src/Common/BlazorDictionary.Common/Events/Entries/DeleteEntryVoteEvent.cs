namespace BlazorDictionary.Common.Events.Entries;

public class DeleteEntryVoteEvent
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
}
