using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorDictionary.Infrastructure.Persistence.EntityConfigurations;

public class EmailConfirmationConfiguration : BaseEntityConfiguration<EmailConfirmation>
{
    public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        base.Configure(builder);

        builder.ToTable("EmailConfirmations", BlazorDictionaryDbContext.DEFAULT_SCHEMA);
    }
}
