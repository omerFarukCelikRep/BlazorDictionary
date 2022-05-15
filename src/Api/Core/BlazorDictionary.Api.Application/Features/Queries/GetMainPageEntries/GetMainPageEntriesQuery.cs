using BlazorDictionary.Common.Models.Pages;
using BlazorDictionary.Common.Models.Queries;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Queries.GetMainPageEntries;

public class GetMainPageEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryDetailsViewModel>>
{
    public GetMainPageEntriesQuery(Guid? userId, int page, int pageSize) : base(page, pageSize)
    {
        UserId = userId;
    }

    public Guid? UserId { get; set; }
}
