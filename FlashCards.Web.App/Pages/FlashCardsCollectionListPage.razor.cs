using FlashCards.Web.Bl.ApiClient;
using FlashCards.Web.Bl.Facades;
using Microsoft.AspNetCore.Components;

namespace FlashCards.Web.App.Pages;

public partial class FlashCardsCollectionListPage : ComponentBase
{
	[Inject] private CardWebFacade CardFacade { get; set; } = null!;
	[Inject] private CardCollectionWebFacade CardCollectionFacade { get; set; } = null!;
	private ICollection<CardListModel>? Cards { get; set; }
	private ICollection<CardListModel>? FilteredCards { get; set; }

	[Parameter] public Guid? CollectionId { get; set; }

	private List<CardCollectionListModel>? CardCollections { get; set; }
	private int currentPage = 1;

	protected override async Task OnInitializedAsync()
	{
		await LoadCollections(currentPage);
		await base.OnInitializedAsync();
		FilteredCards = Cards;
	}

	private async Task HandlePageChange(int newPageNummber)
	{
		currentPage = newPageNummber;
		await LoadCollections(currentPage);
	}

	private async Task LoadCollections(int pageNumber)
	{
		Cards = null;
		//Cards = (List<CardListModel>?)await CardFacade.GetAllAsync(pageNumber: pageNumber, pageSize: 9);

		if (CollectionId.HasValue && CollectionId.Value != Guid.Empty)
		{
			var collectionDetail = await CardCollectionFacade.GetByIdAsync(CollectionId.Value);
			Cards = collectionDetail.Cards
				.Select(detail => new CardListModel
				{
					Id = detail.Id,
					Question = detail.Question,
					CorrectAnswer = detail.CorrectAnswer,
					Description = detail.Description,
					AnswerTypeEnum = detail.AnswerTypeEnum,
					QuestionTypeEnum = detail.QuestionTypeEnum
				})
				.ToList();
		}
		else
		{
			// Otherwise, load all cards
			Cards = (List<CardListModel>?)await CardFacade.GetAllAsync(pageNumber: pageNumber, pageSize: 9);
		}
		StateHasChanged();
	}

	private void PerformSearch(string searchText)
	{
		if (string.IsNullOrWhiteSpace(searchText))
		{
			FilteredCards = Cards;
		}
		else
		{
			FilteredCards = Cards
				.Where(c => c.Question.Contains(searchText, StringComparison.OrdinalIgnoreCase))
				.ToList();
		}
	}
	
	private void OnNewCardClicked()
	{
		Console.WriteLine("New Collection button clicked!");
	}

	private void OnDeleteClicked()
	{
		Console.WriteLine("Delete button clicked!");
	}

}