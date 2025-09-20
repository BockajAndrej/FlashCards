using Microsoft.AspNetCore.Components;

namespace FlashCards.Web.App.Components.ShowHideComponents;

public partial class ModalComponent : ComponentBase
{
	[Parameter] public bool IsVisible { get; set; }

	[Parameter] public EventCallback OnClose { get; set; }

	[Parameter] public RenderFragment? ChildContent { get; set; }

	private async Task Close()
	{
		await OnClose.InvokeAsync();
	}

	private async Task HandleBackdropClick()
	{
		// Optionally close when clicking outside the modal
		await Close();
	}
}