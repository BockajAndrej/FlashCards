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

        // 1. Aplikácia filtra
        if (filter != null)
            query = query.Where(filter);

        // 2. Eager loading súvisiacich entít
        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty.Trim());
        }

        // 3. Aplikácia zoradenia
        if (orderBy != null)
            query = orderBy(query);
        
        query = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        
        // 4. Projekcia do TModel a vykonanie dotazu
        IQueryable<TModel>? projectedQuery = query.ProjectTo<TModel>(mapper.ConfigurationProvider);

        return projectedQuery;
    }

    public async Task<TModel?> GetByIdAsync(Guid id, string? includeProperties = null)
    {
        var entity = await dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
            return default;

        // Použije sa AutoMapper na mapovanie TEntity na TModel.
        return mapper.Map<TModel>(entity);
    }

    /// <summary>
    /// Direct access to db only with one query
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<TModel> SaveAsync(TModel model)
    {
        // Krok 1: Mapovanie TModel na TEntity
        var entity = mapper.Map<TEntity>(model);

        // Krok 2: Určenie, či ide o novú alebo existujúcu entitu
        // Predpokladáme, že entita má vlastnosť "Id"
        var idProperty = entity.GetType().GetProperty("Id");

        // Získanie hodnoty ID. Ak je 0 (alebo default), ide o nový záznam.
        var idValue = (Guid)(idProperty?.GetValue(entity) ?? throw new InvalidOperationException());

        if (idValue == Guid.Empty)
        {
            // Novy zaznam, pridáme ho do DbSet-u.
            dbContext.Set<TEntity>().Add(entity);
        }
        else
        {
            // EF Core ho pripojí k zoznamu sledovaných entít v stave "Modified".
            dbContext.Set<TEntity>().Update(entity);
        }

        // Krok 4: Uloženie zmien do databázy
        await dbContext.SaveChangesAsync();

        // Krok 5: Mapovanie aktualizovanej entity (s vygenerovaným ID) späť na TModel
        // Toto je kľúčové pre vrátenie TModel s platným ID (ak ide o nový záznam).
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