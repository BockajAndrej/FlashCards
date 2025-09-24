using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashCards.WebBlazor.Bl.Facades;

public class CardCollectionWebFacade(ICardCollectionApiClient apiClient) : IWebFacade<CardCollectionListModel, CardCollectionDetailModel>
{
	public async Task DeleteAsync(Guid id)
	{
		await apiClient.CardCollectionDELETEAsync(id);
	}

	public async Task<ICollection<CardCollectionListModel>> GetAllAsync(string? filterAtrib = null, string? filter = null, string? orderBy = null, bool? sortDesc = null,
		int? pageNumber = null, int? pageSize = null)
	{
		return await apiClient.CardCollectionAllAsync(filterAtrib, filter, orderBy, sortDesc, pageNumber, pageSize);
	}

	public async Task<CardCollectionDetailModel> GetByIdAsync(Guid id)
	{
		return await apiClient.CardCollectionGETAsync(id);
	}

	public async Task<int> GetCountAsync(string? strFilterAtrib = null, string? strFilter = null)
	{
		return await apiClient.Count2Async(strFilterAtrib, strFilter);
	}

	public async Task<Guid> SaveToApiAsync(CardCollectionDetailModel data)
	{
		var result = await apiClient.CardCollectionPOSTAsync(data);
		return (Guid)result.Id!;
	}
}