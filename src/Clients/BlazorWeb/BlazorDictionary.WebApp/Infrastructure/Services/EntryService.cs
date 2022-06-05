using BlazorDictionary.Common.Models.Pages;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.Common.Models.RequestModels;
using BlazorDictionary.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorDictionary.WebApp.Infrastructure.Services;

public class EntryService : IEntryService
{
    private readonly HttpClient _client;

    public EntryService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Guid> CreateEntry(CreateEntryCommand createEntryCommand)
    {
        var response = await _client.PostAsJsonAsync("api/entries/CreateEntry", createEntryCommand);

        if (!response.IsSuccessStatusCode)
        {
            return Guid.Empty;
        }

        var guid = await response.Content.ReadAsStringAsync();

        return new Guid(guid.Trim('"'));
    }

    public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand)
    {
        var response = await _client.PostAsJsonAsync("api/entries/CreateEntryComment", createEntryCommentCommand);

        if (!response.IsSuccessStatusCode)
        {
            return Guid.Empty;
        }

        var guid = await response.Content.ReadAsStringAsync();

        return new Guid(guid.Trim('"'));
    }

    public async Task<List<GetEntriesViewModel>> GetEntries()
    {
        var result = await _client.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/entries?isTodaysEntries=false&count=30");

        return result;
    }

    public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
    {
        var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/entries/comments/{entryId}?page={page}&pageSize={pageSize}");

        return result;
    }

    public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
    {
        var result = await _client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entries/{entryId}");

        return result;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
    {
        var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entries/mainpageentries?page={page}&pageSize={pageSize}");

        return result;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string username = null)
    {
        var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entries/UserEntries?userName={username}&page={page}&pageSize={pageSize}");

        return result;
    }

    public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
    {
        var result = await _client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entries/Search?searchText={searchText}");

        return result;
    }
}
