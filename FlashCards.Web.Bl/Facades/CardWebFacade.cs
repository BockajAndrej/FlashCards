using FlashCards.WEB.BL;
using FlashCards.Web.Bl.Facades.Interfaces;

namespace FlashCards.Web.Bl.Facades;

public class CardWebFacade(ICardApiClient apiClient) : ICardWebFacade
{
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.CardDELETEAsync(id);
    }

    public async Task<List<CardListModel>> GetAllAsync(string? filterAtrib = null, string? filter = null, string? orderBy = null)
    {
        var result = await apiClient.CardAllAsync(filterAtrib, filter, orderBy, null, null, null);
        return result as List<CardListModel> ?? throw new InvalidOperationException();
    }

    public async Task<CardDetailModel> GetByIdAsync(Guid id)
    {
        var result = await apiClient.CardGETAsync(id);
        return result;
    }

    public async Task<Guid> SaveToApiAsync(CardDetailModel data)
    {
        var result = await apiClient.CardPOSTAsync(data);
        return result.Id;
    }
}