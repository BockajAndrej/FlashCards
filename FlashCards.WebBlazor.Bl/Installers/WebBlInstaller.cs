using FlashCards.WebBlazor.Bl.Facades;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.WebBlazor.Bl.Installers;

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