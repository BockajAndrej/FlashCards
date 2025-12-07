using FlashCards.WebBlazor.Bl.ApiClient;

namespace FlashCards.WebBlazor.Bl.Facades.Interfaces;

public interface IWebFacade<TQueryObjects, TListModel, TDetailModel>
{
    public Task DeleteAsync(Guid id);

    public Task<ICollection<TListModel>> GetAllAsync(TQueryObjects queryObject);

    public Task<TDetailModel> GetByIdAsync(Guid id);
    public Task<int> GetCountAsync(TQueryObjects queryObject);

    public Task<Guid> SaveToApiAsync(TDetailModel data);
}