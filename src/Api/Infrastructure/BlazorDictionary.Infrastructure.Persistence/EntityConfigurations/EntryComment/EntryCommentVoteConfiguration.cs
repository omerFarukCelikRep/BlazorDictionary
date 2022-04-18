using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorDictionary.Infrastructure.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentVoteConfiguration : BaseEntityConfiguration<EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryCommentVotes", BlazorDictionaryDbContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.EntryComment).WithMany(x => x.EntryCommentVotes).HasForeignKey(x => x.EntryCommentId);
    }
}