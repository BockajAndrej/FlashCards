using FlashCards.Common.QueryObjects;
using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashCards.WebBlazor.Bl.Facades;

public class CardWebFacade(ICardApiClient apiClient) : IWebFacade<CardQueryObject, CardListModel, CardDetailModel>
{
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.CardDELETEAsync(id);
    }

	public async Task<ICollection<CardListModel>> GetAllAsync(CardQueryObject queryObject)
	{
		return await apiClient.CardAllAsync(queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
	}

    public async Task<CardDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.CardGETAsync(id);
    }

    public async Task<int> GetCountAsync(CardQueryObject queryObject)
    {
        return await apiClient.Count2Async(queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
    }

    public async Task<Guid> SaveToApiAsync(CardDetailModel data)
    {
        if (data.Id == Guid.Empty)
        {
            var result = await apiClient.CardPOSTAsync(data);
            return result.Id;
        }
        await apiClient.CardPUTAsync(data.Id, data);
        return Guid.Empty;
    }
}