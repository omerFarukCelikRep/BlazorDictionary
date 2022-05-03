using BlazorDictionary.Common.Models;

namespace BlazorDictionary.Api.Domain.Models;

public class EntryCommentVote : BaseEntity
{
    public VoteType VoteType { get; set; }
    public Guid EntryCommentId { get; set; }
    public virtual EntryComment EntryComment { get; set; }
    public Guid CreatedById { get; set; }
    public virtual User CreatedBy { get; set; }
}
