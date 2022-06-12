using BlazorDictionary.Api.Application.Features.Commands.Users.ConfirmEmail;
using BlazorDictionary.Api.Application.Features.Queries.GetUserDetail;
using BlazorDictionary.Common.Events.Users;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDictionary.Api.WebApi.Controllers;

public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _mediator.Send(new GetUserDetailQuery(id));

        return Ok(user);
    }

    [HttpGet("UserName/{userName}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByUserName(string userName)
    {
        var user = await _mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));

        return Ok(user);
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
    {
        var response = await _mediator.Send(loginUserCommand);

        return Ok(response);
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand createUserCommand)
    {
        var guid = await _mediator.Send(createUserCommand);

        return Ok(guid);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
    {
        var guid = await _mediator.Send(updateUserCommand);

        return Ok(guid);
    }

    [HttpPost("Confirm")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(Guid id)
    {
        var result = await _mediator.Send(new ConfirmEmailCommand { ConfirmationId = id });

        return Ok(result);
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand changeUserPasswordCommand)
    {
        if (!changeUserPasswordCommand.UserId.HasValue)
        {
            changeUserPasswordCommand.UserId = UserId;
        }

        var result = await _mediator.Send(changeUserPasswordCommand);

        return Ok(result);
    }
}
