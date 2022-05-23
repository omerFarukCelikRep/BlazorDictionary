using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Api.Application.Features.Queries.SearchBySubject;
public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public SearchEntryQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }
    public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
    {
        var result = _entryRepository.GetAll(x => EF.Functions.Like(x.Subject, $"{request.SearchText}%")).Select(x => new SearchEntryViewModel
        {
            Id = x.Id,
            Subject = x.Subject
        });

        return await result.ToListAsync(cancellationToken);
    }
}
