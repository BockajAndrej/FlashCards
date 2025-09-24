using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FlashCards.WebBlazor.App;
using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Installers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Syncfusion.Blazor;

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
        scopes: new[] { builder.Configuration["IdentityServer:Scope"]  ?? throw new InvalidOperationException()}!);
});

builder.Services.AddHttpClient<ICardCollectionApiClient, CardCollectionApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler(serviceProvider =>
{
    var authHandler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>();
    return authHandler.ConfigureHandler(
        authorizedUrls: new[] { apiBaseUrl },
        scopes: new[] {  builder.Configuration["IdentityServer:Scope"] ?? throw new InvalidOperationException()});
});


builder.Services.AddHttpClient<ICompletedLessonApiClient, CompletedLessonApiClient>(client =>
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
});

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration.GetSection("Licenses")["SyncfusionKey"]);
builder.Services.AddSyncfusionBlazor();

WebBlInstaller.Install(builder.Services, apiBaseUrl);

await builder.Build().RunAsync();