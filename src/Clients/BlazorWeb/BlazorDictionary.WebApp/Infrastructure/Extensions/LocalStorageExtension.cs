using Blazored.LocalStorage;

namespace BlazorDictionary.WebApp.Infrastructure.Extensions;

public static class LocalStorageExtension
{
    public const string TokenName = "token";
    public const string UserName = "username";
    public const string UserId = "token";

    public static bool IsUserLoggedIn(this ISyncLocalStorageService syncLocalStorageService)
    {
        return !string.IsNullOrEmpty(GetToken(syncLocalStorageService));
    }

    public static string GetUserName(this ISyncLocalStorageService syncLocalStorageService)
    {
        return syncLocalStorageService.GetItem<string>(UserName);
    }

    public static async Task<string> GetUserName(this ILocalStorageService localStorageService)
    {
        return await localStorageService.GetItemAsync<string>(UserName);
    }

    public static void SetUsername(this ISyncLocalStorageService syncLocalStorageService, string value)
    {
        syncLocalStorageService.SetItem(UserName, value);
    }

    public static async Task SetUsername(this ILocalStorageService localStorageService, string value)
    {
        await localStorageService.SetItemAsync(UserName, value);
    }

    public static Guid GetUserId(this ISyncLocalStorageService syncLocalStorageService)
    {
        return syncLocalStorageService.GetItem<Guid>(UserId);
    }

    public static async Task<Guid> GetUserId(this ILocalStorageService syncLocalStorageService)
    {
        return await syncLocalStorageService.GetItemAsync<Guid>(UserId);
    }

    public static void SetUserId(this ISyncLocalStorageService syncLocalStorageService, Guid id)
    {
        syncLocalStorageService.SetItem(UserId, id);
    }

    public static async Task SetUserId(this ILocalStorageService localStorageService, Guid id)
    {
        await localStorageService.SetItemAsync(UserId, id);
    }

    public static string GetToken(this ISyncLocalStorageService syncLocalStorageService)
    {
        var token = syncLocalStorageService.GetItem<string>(TokenName);

        if (string.IsNullOrEmpty(token))
        {
            token = "";
        }

        return token;
    }

    public static async Task<string> GetToken(this ILocalStorageService localStorageService)
    {
        var token = await localStorageService.GetItemAsync<string>(TokenName);

        if (string.IsNullOrEmpty(token))
        {
            token = "";
        }

        return token;
    }

    public static void SetToken(this ISyncLocalStorageService syncLocalStorageService, string value)
    {
        syncLocalStorageService.SetItem(TokenName, value);
    }

    public static async Task SetToken(this ILocalStorageService localStorageService, string value)
    {
        await localStorageService.SetItemAsync(TokenName, value);
    }
}
