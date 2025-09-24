using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FlashCards.WebBlazor.App;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("IdentityServer", options.ProviderOptions);
});

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration.GetSection("Licenses")["SyncfusionKey"]);
builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();