using BlazorDictionary.Api.Application.Features.Commands.Users.ConfirmEmail;
using BlazorDictionary.Common.Events.Users;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDictionary.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
    {
        var response = _mediator.Send(loginUserCommand);

        return Ok(response);
    }


    [HttpPost]
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
