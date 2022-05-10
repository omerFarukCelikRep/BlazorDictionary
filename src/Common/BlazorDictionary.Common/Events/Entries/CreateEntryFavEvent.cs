namespace BlazorDictionary.Common.Events.Entries;

public class CreateEntryFavEvent
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
}
