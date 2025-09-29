using Microsoft.AspNetCore.Components;

namespace FlashCards.WebBlazor.App.Pages;

public partial class PlayFlashCardsPage : ComponentBase
{
    [Parameter] public Guid CardCollectionId { get; set; }
    
    
}