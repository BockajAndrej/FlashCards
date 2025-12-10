using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FlashCards.WebBlazor.App;
using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Installers;
using FlashCards.WebBlazor.Bl.Mappers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor;
using MudBlazor.Services;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAutoMapper(
    cfg => cfg.LicenseKey = builder.Configuration.GetSection("Licenses")["Automapper"],
    typeof(CollectionWebMapperProfile));

builder.Logging.AddFilter("LuckyPennySoftware.AutoMapper.License", LogLevel.None);

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

builder.Services.AddHttpClient<ICollectionApiClient, CollectionApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler(serviceProvider =>
{
    var authHandler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>();
    return authHandler.ConfigureHandler(
        authorizedUrls: new[] { apiBaseUrl },
        scopes: new[] {  builder.Configuration["IdentityServer:Scope"] ?? throw new InvalidOperationException()});
});


builder.Services.AddHttpClient<IAttemptApiClient, AttemptApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler(serviceProvider =>
{
    var authHandler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>();
    return authHandler.ConfigureHandler(
        authorizedUrls: new[] { apiBaseUrl },
        scopes: new[] { "FlashCardsApiScope" });
});

builder.Services.AddHttpClient<IRecordApiClient, RecordApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler(serviceProvider =>
{
    var authHandler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>();
    return authHandler.ConfigureHandler(
        authorizedUrls: new[] { apiBaseUrl },
        scopes: new[] { "FlashCardsApiScope" });
});

builder.Services.AddHttpClient<IFilterApiClient, FilterApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler(serviceProvider =>
{
    var authHandler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>();
    return authHandler.ConfigureHandler(
        authorizedUrls: new[] { apiBaseUrl },
        scopes: new[] { "FlashCardsApiScope" });
});

builder.Services.AddHttpClient<ITagApiClient, TagApiClient>(client =>
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
    options.AuthenticationPaths.LogOutSucceededPath = "/";
});

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 400;
    config.SnackbarConfiguration.ShowTransitionDuration = 400;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

WebBlInstaller.Install(builder.Services, apiBaseUrl);

await builder.Build().RunAsync();