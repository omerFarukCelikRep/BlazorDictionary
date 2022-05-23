using BlazorDictionary.Api.Application.Features.Commands.Entries.DeleteVote;
using BlazorDictionary.Api.Application.Features.Commands.EntryComments.DeleteVote;
using BlazorDictionary.Common.Models;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDictionary.Api.WebApi.Controllers;

public class VotesController : BaseController
{
    private readonly IMediator _mediator;

    public VotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Entry/{entryId:guid}")]
    public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
    {
        var result = await _mediator.Send(new CreateEntryVoteCommand(entryId, voteType, UserId.Value));

        return Ok(result);
    }

    [HttpPost("EntryComment/{entryCommentId:guid}")]
    public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
    {
        var result = await _mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, voteType, UserId.Value));

        return Ok(result);
    }

    [HttpPost("DeleteEntryVote/{entryId:guid}")]
    public async Task<IActionResult> DeleteEntryVote(Guid entryId)
    {
        await _mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));

        return Ok();
    }

    [HttpPost("DeleteEntryCommentVote/{entryCommentId:guid}")]
    public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
    {
        await _mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));

        return Ok();
    }

}
