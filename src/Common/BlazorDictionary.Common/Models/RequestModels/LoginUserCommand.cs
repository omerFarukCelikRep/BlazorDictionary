using BlazorDictionary.Common.Models.Queries;
using MediatR;

namespace BlazorDictionary.Common.Models.RequestModels;

public class LoginUserCommand : IRequest<LoginUserViewModel>
{
    public string Email { get; private set; }
    public string Password { get; private set; }

    public LoginUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public LoginUserCommand()
    {

    }
}
