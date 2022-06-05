using BlazorDictionary.Common.Events.Users;
using BlazorDictionary.Common.Infrastracture.Exceptions;
using BlazorDictionary.Common.Infrastracture.Results;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.WebApp.Infrastructure.Services.Interfaces;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorDictionary.WebApp.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
    {
        var userDetail = await _client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");

        return userDetail;
    }

    public async Task<UserDetailViewModel> GetUserDetail(string userName)
    {
        var userDetail = await _client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/username/{userName}");

        return userDetail;
    }

    public async Task<bool> UpdateUser(UserDetailViewModel userDetail)
    {
        var response = await _client.PostAsJsonAsync($"/api/user/update", userDetail);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
    {
        var command = new ChangeUserPasswordCommand(null, oldPassword, newPassword);

        var httpResponse = await _client.PostAsJsonAsync($"/api/User/ChangePassword", command);

        if (httpResponse.IsSuccessStatusCode)
        {
            return httpResponse.IsSuccessStatusCode;
        }

        if (httpResponse.StatusCode != HttpStatusCode.BadRequest)
        {
            return false;
        }

        var responseString = await httpResponse.Content.ReadAsStringAsync();
        var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseString);

        responseString = validation.FlattenErrors;

        throw new DatabaseValidationException(responseString);
    }
}