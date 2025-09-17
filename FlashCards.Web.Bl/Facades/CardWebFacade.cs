using FlashCards.WEB.BL;
using FlashCards.Web.Bl.Facades.Interfaces;

namespace FlashCards.Web.Bl.Facades;

public class CardWebFacade(ICardApiClient apiClient) : IWebFacade<CardListModel, CardDetailModel>
{
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.CardDELETEAsync(id);
    }

    public async Task<ICollection<CardListModel>> GetAllAsync(string? filterAtrib = null, string? filter = null, string? orderBy = null)
    {
        return await apiClient.CardAllAsync(filterAtrib, filter, orderBy, null, null, null);
    }

    public async Task<CardDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.CardGETAsync(id);
    }

    public async Task<Guid> SaveToApiAsync(CardDetailModel data)
    {
        var result = await apiClient.CardPOSTAsync(data);
        return result.Id;
    }
}