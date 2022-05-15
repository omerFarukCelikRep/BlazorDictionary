using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Contexts;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories;

public class EmailConfirmationRepository : BaseRepository<EmailConfirmation>, IEmailConfirmationRepository
{
    public EmailConfirmationRepository(BlazorDictionaryDbContext context) : base(context) { }
}
