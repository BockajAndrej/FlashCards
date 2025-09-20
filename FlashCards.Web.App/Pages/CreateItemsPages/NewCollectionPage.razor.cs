using FlashCards.Web.Bl.ApiClient;
using FlashCards.Web.Bl.Facades;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace FlashCards.Web.App.Pages.CreateItemsPages
{
	public partial class NewCollectionPage : ComponentBase
	{
		// 1. Inject NavigationManager
		[Inject] private NavigationManager Navigation { get; set; } = null!;

		[Inject] private CardCollectionWebFacade CardCollectionWebFacade { get; set; } = null!;

		private CardCollectionDetailModel Collection { get; set; } = new();

		private string FixedUserId = "04dc81f5-1e6a-4e9e-b483-37fbc06ca322"; // As requested

		protected override void OnInitialized()
		{
			// Set default dates for user convenience
			Collection.StartTimeForAcceptedAnswers = DateTimeOffset.Now;
			Collection.EndTimeForAcceptedAnswers = DateTimeOffset.Now.AddDays(7);
		}

		private async Task HandleValidSubmit()
		{
			// Set properties required by the API before sending
			Collection.Id = Guid.Empty;
			Collection.UserId = FixedUserId;
			Collection.Cards = new List<CardListModel>();

			try
			{
				await CardCollectionWebFacade.SaveToApiAsync(Collection);
				Navigation.NavigateTo("/CollectionsPage"); // Go back to the list after success
			}
			catch (Exception ex)
			{
				// Log the error or show an error message to the user
				Console.WriteLine($"Error saving collection: {ex.Message}");
			}
		}

		private void HandleInvalidSubmit()
		{
			Console.WriteLine("Validation failed! Please check the form for errors.");
		}

		// 2. Add the method for the close button
		private void GoBack()
		{
			Navigation.NavigateTo("/CollectionsPage");
		}
	}
}