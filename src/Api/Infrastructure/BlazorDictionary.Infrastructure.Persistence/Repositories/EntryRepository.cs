using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Contexts;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories;

public class EntryRepository : BaseRepository<Entry>, IEntryRepository
{
    public EntryRepository(BlazorDictionaryDbContext context) : base(context) { }
}
