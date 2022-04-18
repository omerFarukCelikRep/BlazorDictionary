using BlazorDictionary.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorDictionary.Infrastructure.Persistence.EntityConfigurations.Entry;

public class EntryConfiguration : BaseEntityConfiguration<Api.Domain.Models.Entry>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.Entry> builder)
    {
        base.Configure(builder);

        builder.ToTable("Entries", BlazorDictionaryDbContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.CreatedBy).WithMany(x => x.Entries).HasForeignKey(x => x.CreatedById);
    }
}
