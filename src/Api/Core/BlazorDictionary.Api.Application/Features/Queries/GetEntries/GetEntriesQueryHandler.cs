﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorDictionary.Api.Application.Features.Queries.GetEntries;

public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
{
    private readonly IEntryRepository _entryRepository;
    private readonly IMapper _mapper;

    public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        _entryRepository = entryRepository;
        _mapper = mapper;
    }

    public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        if (request.IsTodaysEntries)
        {
            query = query
                .Where(x => x.CreateDate >= DateTime.Now.Date)
                .Where(x => x.CreateDate <= DateTime.Now.AddDays(1).Date);
        }

        query = query
            .Include(x => x.EntryComments)
            .OrderBy(x => Guid.NewGuid())
            .Take(request.Count);

        return await query
            .ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}