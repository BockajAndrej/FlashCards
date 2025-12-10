using FlashCards.Common.QueryObjects;
using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashCards.WebBlazor.Bl.Facades;

public class FilterWebFacade(IFilterApiClient apiClient) : IWebFacade<FilterQueryObject, FilterListModel, FilterDetailModel>
{
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.FilterDELETEAsync(id);
    }

    public async Task<ICollection<FilterListModel>> GetAllAsync(FilterQueryObject queryObject)
    {
        return await apiClient.FilterAllAsync(queryObject.CreatedByIdFilter, queryObject.IsActive, queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
    }

    public async Task<FilterDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.FilterGETAsync(id);
    }

    public async Task<int> GetCountAsync(FilterQueryObject queryObject)
    {
        return await apiClient.Count4Async(queryObject.CreatedByIdFilter, queryObject.IsActive, queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
    }

    public async Task<Guid> SaveToApiAsync(FilterDetailModel data)
    {
        if (data.Id == Guid.Empty)
        {
            var result = await apiClient.FilterPOSTAsync(data);
            return result.Id;
        }
        await apiClient.FilterPUTAsync(data.Id, data);
        return Guid.Empty;
    }
}