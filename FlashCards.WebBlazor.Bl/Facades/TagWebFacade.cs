using FlashCards.Common.QueryObjects;
using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashCards.WebBlazor.Bl.Facades;

public class TagWebFacade(ITagApiClient apiClient) : IWebFacade<TagQueryObject, TagListModel, TagDetailModel>
{
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.TagDELETEAsync(id);
    }

    public async Task<ICollection<TagListModel>> GetAllAsync(TagQueryObject queryObject)
    {
        return await apiClient.TagAllAsync(queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
    }

    public async Task<TagDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.TagGETAsync(id);
    }

    public async Task<int> GetCountAsync(TagQueryObject queryObject)
    {
        return await apiClient.Count7Async(queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
    }

    public async Task<Guid> SaveToApiAsync(TagDetailModel data)
    {
        if (data.Id == Guid.Empty)
        {
            var result = await apiClient.TagPOSTAsync(data);
            return result.Id;
        }
        await apiClient.TagPUTAsync(data.Id, data);
        return Guid.Empty;
    }
}