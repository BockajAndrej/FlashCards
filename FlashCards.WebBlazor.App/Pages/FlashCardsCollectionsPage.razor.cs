using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;

namespace FlashCards.WebBlazor.App.Pages;

public partial class FlashCardsCollectionsPage : ComponentBase
{
    [Inject] private CardCollectionWebFacade CardCollectionWebFacade { get; set; } = null!;
    
    private List<string> _collectionOptionsforOrdering = new() {nameof(CardCollectionDetailModel.LastPlayedDateTime), nameof(CardCollectionDetailModel.Title), nameof(CardCollectionDetailModel.LastModifiedDateTime)};
    private string _selectedOptionForOrdering = nameof(CardCollectionDetailModel.LastPlayedDateTime);
    public string SelectedOptionForOrdering 
    {
        get => _selectedOptionForOrdering;
        set
        {
            if (_selectedOptionForOrdering != value)
            {
                _selectedOptionForOrdering = value;
                _ = LoadCollectionData();
            }
        }
    }
    private string SelectedOptionForName { get; set; } = "";

    private IEnumerable<CardCollectionListModel>? _cardCollections;
    private readonly int _totalNumberOfPagesize = 12;
    private int _actualPageNumber = 1;

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
            pageNumber: _actualPageNumber,
            pageSize: _totalNumberOfPagesize);
        StateHasChanged();
    }
}