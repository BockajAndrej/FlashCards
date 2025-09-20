using Microsoft.AspNetCore.Components;

namespace FlashCards.Web.App.Components.Matrixes;

public partial class MatrixComponent<TItem> : ComponentBase
{
    [Parameter]
    public RenderFragment<TItem>? ChildContent { get; set; }

    [Parameter]
    public IEnumerable<TItem>? Items { get; set; }

    [Parameter]
    public int Columns { get; set; } = 3; // Default to 3 for a 3x3 grid
}