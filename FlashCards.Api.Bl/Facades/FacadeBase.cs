using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Bl.Facades;

public abstract class FacadeBase
    <TEntity, TModel>
    (FlashCardsDbContext dbContext, IMapper mapper)
    : IFacade<TEntity, TModel>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Return all filtered and ordered detailModel entities with 
    /// </summary>
    /// <param name="filter"></param>   p => p.Price > 100
    /// <param name="orderBy"></param>  query => query.OrderBy(p => p.Name)
    /// <param name="pageSize"></param>
    /// <param name="includeProperties"></param>
    /// <param name="pageNumber"></param>
    /// navigation attributes of required entity
    /// <returns></returns>
    public async Task<IQueryable<TModel>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageNumber = 1,
        int pageSize = 10,
        string? includeProperties = null)
    {
        // Access to DbSet
        IQueryable<TEntity> query = dbContext.Set<TEntity>();

        if (filter != null)
            query = query.Where(filter);

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty.Trim());
        }

        if (orderBy != null)
            query = orderBy(query);
        else 
            query = query.OrderBy(l => l.Id);
        
        query = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        
        IQueryable<TModel>? projectedQuery = query.ProjectTo<TModel>(mapper.ConfigurationProvider);

        return projectedQuery;
    }

    public async Task<TModel?> GetByIdAsync(Guid id, string? includeProperties = null)
    {
        var entity = await dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
            return default;

        return mapper.Map<TModel>(entity);
    }

    /// <summary>
    /// Direct access to db only with one query
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<TModel> SaveAsync(TModel model)
    {
        var entity = mapper.Map<TEntity>(model);
        
        var idProperty = entity.GetType().GetProperty("Id");

        var idValue = (Guid)(idProperty?.GetValue(entity) ?? throw new InvalidOperationException());

        if (idValue == Guid.Empty)
        {
            dbContext.Set<TEntity>().Add(entity);
        }
        else
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        await dbContext.SaveChangesAsync();
        
        mapper.Map(entity, model);

        return model;
    }

    public async Task<bool> DeleteAsync(Guid entityId)
    {
        TEntity? entity = await dbContext.Set<TEntity>().FindAsync(entityId);
        if (entity != null)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
        else
            return false;
        return true;
    }
}