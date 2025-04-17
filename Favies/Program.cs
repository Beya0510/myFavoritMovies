using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Favies.Services;
using Favies;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<GetMoviesService>();
builder.Services.AddScoped<FavoritesService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "https://api.example.com";
    options.ProviderOptions.ClientId = "YOUR_CLIENT_ID";
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("email");
});

await builder.Build().RunAsync();