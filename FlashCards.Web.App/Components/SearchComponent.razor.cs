using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace FlashCards.Web.App.Components;

public partial class SearchComponent : ComponentBase
{
	[Parameter] public string Placeholder { get; set; } = "Search...";

	[Parameter] public EventCallback<string> OnSearchChanged { get; set; }

	private Task HandleInput(ChangeEventArgs e)
	{
		string searchText = e.Value?.ToString() ?? string.Empty;
		return OnSearchChanged.InvokeAsync(searchText);
	}
}