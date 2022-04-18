using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorDictionary.Infrastructure.Persistence.EntityConfigurations.Entry;

public class EntryFavoriteConfiguration : BaseEntityConfiguration<EntryFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryFavorites", BlazorDictionaryDbContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.Entry).WithMany(x => x.EntryFavorites).HasForeignKey(x => x.EntryId);

        builder.HasOne(x => x.CreatedBy).WithMany(x => x.EntryFavorites).HasForeignKey(x => x.CreatedById);
    }
}
