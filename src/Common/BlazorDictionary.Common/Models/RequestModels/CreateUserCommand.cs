using MediatR;

namespace BlazorDictionary.Common.Models.RequestModels;

public class CreateUserCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
