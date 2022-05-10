using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories;

public class EntryCommentRepository : BaseRepository<EntryComment>, IEntryCommentRepository
{
    public EntryCommentRepository(DbContext context) : base(context) { }
}
