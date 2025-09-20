using FlashCards.Web.Bl.ApiClient;
using Microsoft.AspNetCore.Components;

namespace FlashCards.Web.App.Components.Matrixes;

public partial class MatrixCardItem : ComponentBase
{
	[Parameter] public CardListModel CardList { get; set; } = null!;
}