namespace BlazorDictionary.Common.Events.Entries;

public class DeleteEntryFavEvent
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
}
