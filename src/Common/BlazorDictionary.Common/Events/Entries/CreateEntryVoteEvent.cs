using BlazorDictionary.Common.Models;

namespace BlazorDictionary.Common.Events.Entries;

public class CreateEntryVoteEvent
{
    public Guid EntryId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedBy { get; set; }
}
