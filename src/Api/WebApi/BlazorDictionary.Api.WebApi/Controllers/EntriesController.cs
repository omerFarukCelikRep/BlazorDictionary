using BlazorDictionary.Api.Application.Features.Queries.GetEntries;
using BlazorDictionary.Api.Application.Features.Queries.GetEntryComments;
using BlazorDictionary.Api.Application.Features.Queries.GetEntryDetail;
using BlazorDictionary.Api.Application.Features.Queries.GetMainPageEntries;
using BlazorDictionary.Api.Application.Features.Queries.GetUserEntries;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDictionary.Api.WebApi.Controllers;

public class EntriesController : BaseController
{
    private readonly IMediator _mediator;

    public EntriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
    {
        var entries = await _mediator.Send(query);

        return Ok(entries);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEntryDetailQuery(id, UserId));

        return Ok(result);
    }

    [HttpGet("MainPageEntries")]
    public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
    {
        var entries = await _mediator.Send(new GetMainPageEntriesQuery(UserId, page, pageSize));

        return Ok(entries);
    }

    [HttpGet("Comments/{id:guid}")]
    public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
    {
        var result = await _mediator.Send(new GetEntryCommentsQuery(id, UserId, page, pageSize));

        return Ok(result);
    }

    [HttpGet("UserEntries")]
    public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
    {
        if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
        {
            userId = UserId.Value;
        }

        var result = await _mediator.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));

        return Ok(result);
    }

    [HttpGet("Search")]
    public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    [Route("CreateEntry")]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand createEntryCommand)
    {
        if (!createEntryCommand.CreatedById.HasValue)
        {
            createEntryCommand.CreatedById = UserId;
        }

        var result = await _mediator.Send(createEntryCommand);

        return Ok(result);
    }

    [HttpPost]
    [Route("CreateEntryComment")]
    public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand createEntryCommentCommand)
    {
        if (!createEntryCommentCommand.CreatedById.HasValue)
        {
            createEntryCommentCommand.CreatedById = UserId;
        }

        var result = await _mediator.Send(createEntryCommentCommand);

        return Ok(result);
    }
}
