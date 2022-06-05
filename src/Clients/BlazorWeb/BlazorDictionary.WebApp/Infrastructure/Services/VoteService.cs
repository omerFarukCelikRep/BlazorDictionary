using BlazorDictionary.Common.Models;
using BlazorDictionary.WebApp.Infrastructure.Services.Interfaces;

namespace BlazorDictionary.WebApp.Infrastructure.Services;

public class VoteService : IVoteService
{
    private readonly HttpClient _client;

    public VoteService(HttpClient client)
    {
        _client = client;
    }

    public async Task DeleteEntryVote(Guid entryId)
    {
        var response = await _client.PostAsync($"/api/Votes/DeleteEntryVote/{entryId}", null);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("DeleteEntryVote error");
        }
    }

    public async Task CreateEntryUpVote(Guid entryId)
    {
        await CreateEntryVote(entryId, VoteType.UpVote);
    }

    public async Task CreateEntryDownVote(Guid entryId)
    {
        await CreateEntryVote(entryId, VoteType.DownVote);
    }

    public async Task CreateEntryCommentUpVote(Guid entryCommentId)
    {
        await CreateEntryCommentVote(entryCommentId, VoteType.UpVote);
    }

    public async Task CreateEntryCommentDownVote(Guid entryCommentId)
    {
        await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
    }

    private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
    {
        var result = await _client.PostAsync($"/api/votes/entry/{entryId}?voteType={voteType}", null);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception("CreateEntryVote error");
        }

        return result;
    }

    private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
    {
        var result = await _client.PostAsync($"/api/votes/entrycomment/{entryCommentId}?voteType={voteType}", null);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception("CreateEntryCommentVote error");
        }

        return result;
    }

    public Task DeleteEntryCommentVote(Guid entryCommentId)
    {
        throw new NotImplementedException();
    }
}
