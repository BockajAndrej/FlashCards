using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FlashCards.Web.App;
using FlashCards.Web.Bl.Installers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string apiBaseUrl = builder.Configuration.GetSection("ApiBaseUrl").Value ?? throw new InvalidOperationException("ApiBaseUrl is not configured.");
ConfigureDependencies(builder.Services, apiBaseUrl);

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

void ConfigureDependencies(IServiceCollection serviceCollection, string apiBaseUrlAddress)
{
    WebBlInstaller.Install(serviceCollection, apiBaseUrlAddress);
}