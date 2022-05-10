using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Events.Users;
using BlazorDictionary.Common.Infrastracture;
using BlazorDictionary.Common.Infrastracture.Exceptions;
using MediatR;

namespace BlazorDictionary.Api.Application.Features.Commands.Users.ChangePassword;

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.UserId);

        var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);

        if (dbUser == null)
        {
            throw new DatabaseValidationException("User Not Found");
        }

        var encOldPasword = PasswordEncryptor.Encrypt(request.OldPassword);

        if (dbUser.Password != encOldPasword)
        {
            throw new DatabaseValidationException("Old Password is wrong");
        }

        dbUser.Password = PasswordEncryptor.Encrypt(request.NewPassword);



        return await _userRepository.UpdateAsync(dbUser) > 0;
    }
}
