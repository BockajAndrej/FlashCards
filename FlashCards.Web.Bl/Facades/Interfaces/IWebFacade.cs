namespace FlashCards.Web.Bl.Facades.Interfaces;

public interface IWebFacade<TListModel, TDetailModel>
{
    public Task DeleteAsync(Guid id);

    public Task<ICollection<TListModel>> GetAllAsync(string? filterAtrib = null, string? filter = null, string? orderBy = null);

    public Task<TDetailModel> GetByIdAsync(Guid id);

    public Task<Guid> SaveToApiAsync(TDetailModel data);
}