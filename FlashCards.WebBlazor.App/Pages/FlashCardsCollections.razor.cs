using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;

namespace FlashCards.WebBlazor.App.Pages;

public partial class FlashCardsCollections : ComponentBase
{
    [Inject] private CardCollectionWebFacade CardCollectionWebFacade { get; set; } = null!;
    
    private List<string> _collectionOptionsforOrdering = new() {"Recent", "Name", "Creation"};
    private string SelectedOptionForOrdering { get; set; } = "Recent";
    private string SelectedOptionForName { get; set; } = "";

    private IEnumerable<CardCollectionListModel>? _cardCollections;
    private readonly int _totalNumberOfPagesize = 12;

    protected override async Task OnInitializedAsync()
    {
        await LoadCollectionData();
    }

    private async Task FilterSearch(InputEventArgs args)
    {
        SelectedOptionForName = args.Value;

        await LoadCollectionData();
        
        StateHasChanged();
    }

    private async Task LoadCollectionData()
    {
        _cardCollections = await CardCollectionWebFacade.GetAllAsync(
            filterAtrib: nameof(CardCollectionListModel.Title), 
            filter: SelectedOptionForName,
            orderBy: SelectedOptionForOrdering,
            sortDesc: false,
            pageNumber: 1,
            pageSize: _totalNumberOfPagesize);
    }
}