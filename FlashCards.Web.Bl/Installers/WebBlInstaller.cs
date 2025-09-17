using FlashCards.WEB.BL;
using FlashCards.Web.Bl.Facades;
using FlashCards.Web.Bl.Facades.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Web.Bl.Installers;

public static class WebBlInstaller
{
    public static void Install(IServiceCollection serviceCollection, string apiBaseUrl)
    {
        serviceCollection.AddTransient<ICardApiClient, CardApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new CardApiClient(apiBaseUrl, client);
        });
         
        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<CardWebFacade>()
                .AddClasses(classes => classes.AssignableTo(typeof(IWebFacade<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
    }
    private static HttpClient CreateApiHttpClient(string apiBaseUrl)
    {
        return new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    }
}