using FlashCards.WEB.BL;
using FlashCards.Web.Bl.Facades;
using Microsoft.AspNetCore.Components;

namespace FlashCards.Web.App.Pages;

public partial class FlashCardsCollectionListPage : ComponentBase
{
    [Inject]
    private CardWebFacade CardFacade { get; set; } = null!;
    [Inject]
    private CardCollectionWebFacade CardCollectionFacade { get; set; } = null!;
    private List<CardListModel>? Cards { get; set; }
    
    private List<CardCollectionListModel>? CardCollections { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        CardCollections = (List<CardCollectionListModel>?)await CardCollectionFacade.GetAllAsync();
        Cards = (List<CardListModel>?)await CardFacade.GetAllAsync();
        
        await base.OnInitializedAsync();
    }
}