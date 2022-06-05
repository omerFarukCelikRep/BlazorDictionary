using BlazorDictionary.Common.Infrastracture.Exceptions;
using BlazorDictionary.Common.Infrastracture.Results;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.Common.Models.RequestModels;
using BlazorDictionary.WebApp.Infrastructure.Extensions;
using BlazorDictionary.WebApp.Infrastructure.Services.Interfaces;
using Blazored.LocalStorage;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorDictionary.WebApp.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _client;
    private readonly ISyncLocalStorageService _syncLocalStorageService;
    public IdentityService(HttpClient client, ISyncLocalStorageService syncLocalStorageService)
    {
        _client = client;
        _syncLocalStorageService = syncLocalStorageService;
    }
    public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

    public string GetUserToken()
    {
        return _syncLocalStorageService.GetToken();
    }

    public string GetUserName()
    {
        return _syncLocalStorageService.GetToken();
    }

    public string GetUserId()
    {
        return _syncLocalStorageService.GetToken();
    }

    public async Task<bool> Login(LoginUserCommand loginUserCommand)
    {
        string responseString;

        var httpResponse = await _client.PostAsJsonAsync("/api/User/Login", loginUserCommand);

        if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
        {
            if (httpResponse.StatusCode != HttpStatusCode.BadRequest)
            {
                return false;
            }

            responseString = await httpResponse.Content.ReadAsStringAsync();

            var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseString);

            responseString = validation.FlattenErrors;

            throw new DatabaseValidationException(responseString);
        }

        responseString = await httpResponse.Content.ReadAsStringAsync();

        var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseString);

        if (string.IsNullOrEmpty(response.Token))
        {
            return false;
        }

        _syncLocalStorageService.SetToken(response.Token);
        _syncLocalStorageService.SetUsername(response.Username);
        _syncLocalStorageService.SetUserId(response.Id);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", response.Username);

        return true;
    }

    public void Logout()
    {
        _syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
        _syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);
        _syncLocalStorageService.RemoveItem(LocalStorageExtension.UserId);

        _client.DefaultRequestHeaders.Authorization = null;
    }
}
