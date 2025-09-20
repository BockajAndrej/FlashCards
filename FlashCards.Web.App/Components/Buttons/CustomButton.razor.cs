using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace FlashCards.Web.App.Components.Buttons;

public partial class CustomButton : ComponentBase
{
	[Parameter] public string Text { get; set; } = "Click Me";

	[Parameter] public string BackgroundColor { get; set; } = "#7B2CBF";

	[Parameter] public string TextColor { get; set; } = "white";

	[Parameter] public string BorderColor { get; set; } = "#5A189A";

	[Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

	[Parameter(CaptureUnmatchedValues = true)]
	public Dictionary<string, object> AdditionalAttributes { get; set; } = new();

	private bool IsSubmitButton => AdditionalAttributes.TryGetValue("type", out var type) && type.ToString() == "submit";
}