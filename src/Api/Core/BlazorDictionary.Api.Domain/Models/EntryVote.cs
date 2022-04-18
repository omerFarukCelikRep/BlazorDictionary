using BlazorDictionary.Common.ViewModels;

namespace BlazorDictionary.Api.Domain.Models
{
    public class EntryVote : BaseEntity
    {
        public VoteType VoteType { get; set; }
        public Guid CreatedById { get; set; }

        public Guid EntryId { get; set; }
        public virtual Entry Entry { get; set; }
    }
}