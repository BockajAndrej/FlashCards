using Microsoft.AspNetCore.Components;

namespace FlashCards.WebBlazor.App.Components;

public partial class CollectionCardComponent : ComponentBase
{
    [Inject] 
    private NavigationManager Navigation { get; set; } = null!;
    [Parameter]
    public string Title { get; set; } = "None";
    [Parameter]
    public string StartTime { get; set; } = "";
    [Parameter]
    public string EndTime { get; set; } = "";
    [Parameter]
    public Guid CollectionId { get; set; }
    [Parameter]
    public int Value { get; set; } = 0;
    
    private void OnCollectionClicked()
    {
        Navigation.NavigateTo($"/FlashCardsPage/{CollectionId}");
    }
}