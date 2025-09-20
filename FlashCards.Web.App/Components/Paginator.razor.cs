using Microsoft.AspNetCore.Components;

namespace FlashCards.Web.App.Components;

public partial class Paginator : ComponentBase
{
    [Parameter]
    public int PageNumber { get; set; }

    [Parameter]
    public EventCallback<int> PageNumberChanged { get; set; }

    private async Task GoToPreviousPage()
    {
        if (PageNumber > 1)
        {
            await PageNumberChanged.InvokeAsync(PageNumber - 1);
        }
    }

    private async Task GoToNextPage()
    {
        await PageNumberChanged.InvokeAsync(PageNumber + 1);
    }
}