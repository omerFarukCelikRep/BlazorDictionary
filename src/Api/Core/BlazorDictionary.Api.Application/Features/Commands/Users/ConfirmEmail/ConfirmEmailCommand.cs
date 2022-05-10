using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Users.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<bool>
{
    public Guid ConfirmationId { get; set; }

}
