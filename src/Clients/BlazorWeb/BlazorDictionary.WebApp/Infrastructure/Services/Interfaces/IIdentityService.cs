using BlazorDictionary.Common.Models.RequestModels;

namespace BlazorDictionary.WebApp.Infrastructure.Services.Interfaces;

public interface IIdentityService
{
    bool IsLoggedIn { get; }

    string GetUserId();
    string GetUserName();
    string GetUserToken();
    Task<bool> Login(LoginUserCommand loginUserCommand);
    void Logout();
}
