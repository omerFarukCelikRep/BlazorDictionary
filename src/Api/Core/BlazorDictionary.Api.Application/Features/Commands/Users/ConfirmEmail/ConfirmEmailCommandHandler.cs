using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Infrastracture.Exceptions;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Users.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailConfirmationRepository _emailConfirmationRepository;

    public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
    {
        _userRepository = userRepository;
        _emailConfirmationRepository = emailConfirmationRepository;
    }
    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var confirmation = await _emailConfirmationRepository.GetByIdAsync(request.ConfirmationId);

        if (confirmation == null)
        {
            throw new DatabaseValidationException("Confirmation Not Found");
        }

        var dbUser = await _userRepository.GetSingleAsync(x => x.Email == confirmation.NewEmailAddress);

        if (dbUser == null)
        {
            throw new DatabaseValidationException("User Not Found with This Email");
        }

        if (dbUser.EmailConfirmed)
        {
            throw new DatabaseValidationException("Email is already confirmed!");
        }

        dbUser.EmailConfirmed = true;

        return await _userRepository.UpdateAsync(dbUser) > 0;
    }
}
