using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Contexts;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories;

public class EntryCommentRepository : BaseRepository<EntryComment>, IEntryCommentRepository
{
    public EntryCommentRepository(BlazorDictionaryDbContext context) : base(context) { }
}
