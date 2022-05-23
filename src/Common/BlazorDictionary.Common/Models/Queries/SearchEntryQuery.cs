using MediatR;

namespace BlazorDictionary.Common.Models.Queries;
public class SearchEntryQuery : IRequest<List<SearchEntryViewModel>>
{
    public SearchEntryQuery()
    {

    }
    public SearchEntryQuery(string searchText)
    {
        SearchText = searchText;
    }

    public string SearchText { get; set; }
}