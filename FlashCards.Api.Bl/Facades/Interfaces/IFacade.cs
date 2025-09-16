using System.Linq.Expressions;

namespace FlashCards.Api.Bl.Facades.Interfaces;

public interface IFacade<TEntity, TModel>
{
    public Task<IQueryable<TModel>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageNumber = 1,
        int pageSize = 10,
        string? includeProperties = null);
    public Task<TModel?> GetByIdAsync(Guid id, string? includeProperties = null);
    public Task<TModel> SaveAsync(TModel model);
    public Task<bool> DeleteAsync(Guid entityId);
}