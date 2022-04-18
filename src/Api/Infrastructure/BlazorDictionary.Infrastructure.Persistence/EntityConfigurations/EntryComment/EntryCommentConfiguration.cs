using BlazorDictionary.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorDictionary.Infrastructure.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryComment>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryComment> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryComments", BlazorDictionaryDbContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.CreatedBy).WithMany(x => x.EntryComments).HasForeignKey(x => x.CreatedById);
        builder.HasOne(x => x.Entry).WithMany(x => x.EntryComments).HasForeignKey(x => x.EntryId);
    }
}
