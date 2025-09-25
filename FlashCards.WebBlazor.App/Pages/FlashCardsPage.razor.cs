using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;

namespace FlashCards.WebBlazor.App.Pages;

public partial class FlashCardsPage : ComponentBase
{
    [Parameter] public Guid CardCollectionId { get; set; }
    [Inject] private CardWebFacade CardWebFacade { get; set; } = null!;
    
    private List<string> _cardOptionsforOrdering = new() {"Recent", nameof(CardDetailModel.Question), nameof(CardDetailModel.CorrectAnswer)};
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

    private IEnumerable<CardListModel>? _cardCollections;
    private readonly int _totalNumberOfPagesize = 12;

    protected override async Task OnInitializedAsync()
    {
        await LoadCollectionData();
    }

    private async Task FilterSearch(InputEventArgs args)
    {
        SelectedOptionForName = args.Value;
        await LoadCollectionData();
    }

    private async Task LoadCollectionData()
    {
        _cardCollections = await CardWebFacade.GetAllAsync(
            filterAtrib: $"{nameof(CardDetailModel.Question)},{nameof(CardDetailModel.CardCollectionId)}",
            filter: $"{SelectedOptionForName},{CardCollectionId}",
            orderBy: SelectedOptionForOrdering,
            sortDesc: false,
            pageNumber: 1,
            pageSize: _totalNumberOfPagesize);
        StateHasChanged();
    }
}