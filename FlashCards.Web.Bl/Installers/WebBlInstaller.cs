using FlashCards.Web.Bl.ApiClient;
using FlashCards.Web.Bl.Facades;
using FlashCards.Web.Bl.Facades.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Web.Bl.Installers;

public static class WebBlInstaller
{
    public static void Install(IServiceCollection serviceCollection, string apiBaseUrl)
    {
        var client = CreateApiHttpClient(apiBaseUrl);
        
        serviceCollection.AddTransient<ICardApiClient, CardApiClient>(_ => new CardApiClient(apiBaseUrl, client));
        serviceCollection.AddTransient<ICardCollectionApiClient, CardCollectionApiClient>(_ => new CardCollectionApiClient(apiBaseUrl, client));
        
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