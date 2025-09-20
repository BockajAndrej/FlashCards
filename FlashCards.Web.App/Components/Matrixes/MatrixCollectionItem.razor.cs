using FlashCards.Web.Bl.ApiClient;
using Microsoft.AspNetCore.Components;

namespace FlashCards.Web.App.Components.Matrixes;

public partial class MatrixCollectionItem : ComponentBase
{
    [Parameter]
    public CardCollectionListModel Collection { get; set; } = null!;
}