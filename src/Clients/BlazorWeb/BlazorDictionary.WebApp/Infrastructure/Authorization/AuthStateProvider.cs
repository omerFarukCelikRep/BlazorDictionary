using BlazorDictionary.WebApp.Infrastructure.Extensions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorDictionary.WebApp.Infrastructure.Authorization;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationState _authenticationState;

    public AuthStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        _authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var apiToken = await _localStorageService.GetToken();

        if (string.IsNullOrEmpty(apiToken))
        {
            return _authenticationState;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadJwtToken(apiToken);

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims, "JwtAuthType"));

        return new AuthenticationState(claimsPrincipal);
    }

    public void NotifyUserLogin(string userName, Guid userId)
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        }, "JwtAuthType"));

        var authState = Task.FromResult(new AuthenticationState(claimsPrincipal));

        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_authenticationState);

        NotifyAuthenticationStateChanged(authState);
    }
}
