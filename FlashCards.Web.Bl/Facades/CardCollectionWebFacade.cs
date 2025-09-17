using FlashCards.WEB.BL;
using FlashCards.Web.Bl.Facades.Interfaces;

namespace FlashCards.Web.Bl.Facades;

public class CardCollectionWebFacade(ICardCollectionApiClient apiClient) : IWebFacade<CardCollectionListModel, CardCollectionDetailModel>
{
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.CardCollectionDELETEAsync(id);
    }

    public async Task<ICollection<CardCollectionListModel>> GetAllAsync(string? filterAtrib = null, string? filter = null, string? orderBy = null)
    {
        return await apiClient.CardCollectionAllAsync(filterAtrib, filter, orderBy, null, null, null);
    }

    public async Task<CardCollectionDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.CardCollectionGETAsync(id);
    }

    public async Task<Guid> SaveToApiAsync(CardCollectionDetailModel data)
    {
        var result = await apiClient.CardCollectionPOSTAsync(data);
        return result.Id;
    }
}