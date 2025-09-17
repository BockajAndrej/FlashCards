using FlashCards.WEB.BL;
using FlashCards.Web.Bl.Facades.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FlashCards.Web.App.Pages;

public partial class FlashCardsCollectionListPage : ComponentBase
{
    [Inject]
    private ICardWebFacade CardFacade { get; set; } = null!;
    private List<CardListModel>? Collections { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        Collections = await CardFacade.GetAllAsync();
        
        await base.OnInitializedAsync();
    }
}