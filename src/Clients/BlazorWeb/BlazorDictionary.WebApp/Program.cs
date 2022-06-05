using BlazorDictionary.WebApp;
using BlazorDictionary.WebApp.Infrastructure.Services;
using BlazorDictionary.WebApp.Infrastructure.Services.Interfaces;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("WebApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WebApiClient:Url"]);
});

builder.Services.AddScoped(serviceProvider =>
{
    var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

    return clientFactory.CreateClient("WebApiClient");
});

builder.Services.AddTransient<IVoteService, VoteService>();
builder.Services.AddTransient<IEntryService, EntryService>();
builder.Services.AddTransient<IFavoriteService, FavoriteService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
