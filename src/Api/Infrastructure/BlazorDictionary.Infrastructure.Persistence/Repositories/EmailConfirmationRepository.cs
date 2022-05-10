using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories;

public class EmailConfirmationRepository : BaseRepository<EmailConfirmation>, IEmailConfirmationRepository
{
    public EmailConfirmationRepository(DbContext context) : base(context) { }
}
