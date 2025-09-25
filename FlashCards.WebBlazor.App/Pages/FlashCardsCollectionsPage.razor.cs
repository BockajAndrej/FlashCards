using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;

namespace FlashCards.WebBlazor.App.Pages;

public partial class FlashCardsCollectionsPage : ComponentBase
{
    [Inject] private CardCollectionWebFacade CardCollectionWebFacade { get; set; } = null!;
    
    private List<string> _collectionOptionsforOrdering = new() {"Recent", nameof(CardCollectionDetailModel.Title), nameof(CardCollectionDetailModel.StartTimeForAcceptedAnswers)};
    private string _selectedOptionForOrdering = "Recent";
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
    private async Task FilterDropDownSearch()
    {
        await LoadCollectionData();
    }

    private async Task LoadCollectionData()
    {
        _cardCollections = await CardCollectionWebFacade.GetAllAsync(
            filterAtrib: nameof(CardCollectionListModel.Title), 
            filter: SelectedOptionForName,
            orderBy: SelectedOptionForOrdering,
            sortDesc: true,
            pageNumber: 1,
            pageSize: _totalNumberOfPagesize);
        StateHasChanged();
    }
}