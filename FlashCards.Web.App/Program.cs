using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FlashCards.Web.App;
using FlashCards.Web.Bl.ApiClient;
using FlashCards.Web.Bl.Installers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string apiBaseUrl = builder.Configuration.GetSection("ApiBaseUrl").Value ?? throw new InvalidOperationException("ApiBaseUrl is not configured.");

builder.Services.AddHttpClient<ICardApiClient, CardApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler(serviceProvider =>
{
    var authHandler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>();
    return authHandler.ConfigureHandler(
        authorizedUrls: new[] { apiBaseUrl },
        scopes: new[] { "FlashCardsApiScope" });
});

builder.Services.AddHttpClient<ICardCollectionApiClient, CardCollectionApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler(serviceProvider =>
{
    var authHandler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>();
    return authHandler.ConfigureHandler(
        authorizedUrls: new[] { apiBaseUrl },
        scopes: new[] { "FlashCardsApiScope" });
});


builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("IdentityServer", options.ProviderOptions);
    options.ProviderOptions.DefaultScopes.Add("FlashCardsApiScope");
});

builder.Services.AddLocalization();
ConfigureDependencies(builder.Services, apiBaseUrl);

var application = builder.Build();

await application.RunAsync();

void ConfigureDependencies(IServiceCollection serviceCollection, string apiBaseUrlAddress)
{
    WebBlInstaller.Install(serviceCollection, apiBaseUrlAddress);
}