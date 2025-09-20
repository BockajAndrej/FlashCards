using FlashCards.Web.Bl.Facades;
using Microsoft.AspNetCore.Components;
using FlashCards.Web.Bl.ApiClient;
using Microsoft.AspNetCore.Components; // Make sure this is included

namespace FlashCards.Web.App.Pages;

public partial class CollectionsPage : ComponentBase
{
	[Inject] private NavigationManager Navigation { get; set; } = null!;
	[Inject] private CardCollectionWebFacade CardCollectionWebFacade { get; set; } = null!;

	// This stores the original data loaded for the current page
	private ICollection<CardCollectionListModel>? Collections { get; set; }

	// This is what the UI should display (it can be filtered)
	// NOTE: For this new server-side search logic, Collections and FilteredCollections
	// will be the same, as the server does the filtering for us.
	private ICollection<CardCollectionListModel>? FilteredCollections { get; set; }

	private int currentPage = 1;
	private string _currentSearchText = string.Empty;

	private bool _showAddCollectionModal = false;

	protected override async Task OnInitializedAsync()
	{
		await LoadCollections(currentPage);
	}

	private async Task HandlePageChange(int newPageNumber)
	{
		await LoadCollections(newPageNumber);
	}

	private async Task LoadCollections(int pageNumber)
	{
		FilteredCollections = null;
		StateHasChanged();

		// The server now does the filtering for us using the _currentSearchText
		var collectionsFromServer = await CardCollectionWebFacade.GetAllAsync(
			filterAtrib: "Title",
			filter: _currentSearchText,
			pageNumber: pageNumber,
			pageSize: 9);

		Collections = collectionsFromServer;
		FilteredCollections = collectionsFromServer;
		currentPage = pageNumber;
		StateHasChanged();
	}

	private async Task PerformSearch(string searchText)
	{
		_currentSearchText = searchText;
		await LoadCollections(1);
	}

	private void OnNewCollectionClicked()
	{
		_showAddCollectionModal = true;
		Navigation.NavigateTo("/NewCollectionPage");
	}

	private async Task OnCloseAddCollectionModal()
	{
		_showAddCollectionModal = false;
		// Optionally reload collections if you want to ensure the list is fresh
		// await LoadCollections(currentPage);
	}

	private async Task HandleAddCollectionSubmit(CardCollectionDetailModel newCollectionData)
	{
		try
		{
			// Call your facade to save the new collection
			Guid newCollectionId = await CardCollectionWebFacade.SaveToApiAsync(newCollectionData);
			Console.WriteLine($"New Collection added with ID: {newCollectionId}");

			_showAddCollectionModal = false; // Close the modal
			await LoadCollections(currentPage); // Reload the list to show the new collection
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error adding collection: {ex.Message}");
			// Handle error (e.g., show a toast notification to the user)
		}
	}

	private void OnDeleteClicked()
	{
		Console.WriteLine("Delete button clicked!");
	}
}