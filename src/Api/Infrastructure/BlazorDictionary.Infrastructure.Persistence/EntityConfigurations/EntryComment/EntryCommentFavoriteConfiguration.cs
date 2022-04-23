using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorDictionary.Infrastructure.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentFavoriteConfiguration : BaseEntityConfiguration<EntryCommentFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryCommentFavorites", BlazorDictionaryDbContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.EntryComment).WithMany(x => x.EntryCommentFavorites).HasForeignKey(x => x.EntryCommentId);
        builder.HasOne(x => x.CreatedBy).WithMany(x => x.EntryCommentFavorites).HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);
    }
}
