using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorDictionary.Infrastructure.Persistence.EntityConfigurations.Entry;

public class EntryVoteConfiguration : BaseEntityConfiguration<EntryVote>
{
    public override void Configure(EntityTypeBuilder<EntryVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryVotes", BlazorDictionaryDbContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.Entry).WithMany(x => x.EntryVotes).HasForeignKey(x => x.EntryId);
    }
}
