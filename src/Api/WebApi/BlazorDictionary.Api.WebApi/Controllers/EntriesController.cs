using BlazorDictionary.Api.Application.Features.Queries.GetEntries;
using BlazorDictionary.Api.Application.Features.Queries.GetMainPageEntries;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDictionary.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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

    [HttpGet("MainPageEntries")]
    public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
    {
        var entries = await _mediator.Send(new GetMainPageEntriesQuery(UserId, page, pageSize));

        return Ok(entries);
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
