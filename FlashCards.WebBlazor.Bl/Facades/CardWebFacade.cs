using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashCards.WebBlazor.Bl.Facades;

public class CardWebFacade(ICardApiClient apiClient) : IWebFacade<CardListModel, CardDetailModel>
{
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.CardDELETEAsync(id);
    }

	public async Task<ICollection<CardListModel>> GetAllAsync(string? filterAtrib = null, string? filter = null, string? orderBy = null, bool? sortDesc = null,
		int? pageNumber = null, int? pageSize = null)
	{
		return await apiClient.CardAllAsync(filterAtrib, filter, orderBy, sortDesc, pageNumber, pageSize);
	}

    public async Task<CardDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.CardGETAsync(id);
    }

    public async Task<int> GetCountAsync(string? strFilterAtrib = null, string? strFilter = null)
    {
        return await apiClient.CountAsync(strFilterAtrib, strFilter);
    }

    public async Task<Guid> SaveToApiAsync(CardDetailModel data)
    {
        var result = await apiClient.CardPOSTAsync(data);
        return (Guid)result.Id!;
    }
}