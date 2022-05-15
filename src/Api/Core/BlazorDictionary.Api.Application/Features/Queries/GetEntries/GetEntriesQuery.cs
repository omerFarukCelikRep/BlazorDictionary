using BlazorDictionary.Common.Models.Queries;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Queries.GetEntries;

public class GetEntriesQuery : IRequest<List<GetEntriesViewModel>>
{
    public bool IsTodaysEntries { get; set; }
    public int Count { get; set; } = 100;
}