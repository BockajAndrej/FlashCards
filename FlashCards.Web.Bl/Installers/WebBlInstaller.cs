using FlashCards.Web.Bl.ApiClient;
using FlashCards.Web.Bl.Facades;
using FlashCards.Web.Bl.Facades.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Web.Bl.Installers;

public static class WebBlInstaller
{
    public static void Install(IServiceCollection serviceCollection, string apiBaseUrl)
    {
        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<CardWebFacade>()
                .AddClasses(classes => classes.AssignableTo(typeof(IWebFacade<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
    }
}