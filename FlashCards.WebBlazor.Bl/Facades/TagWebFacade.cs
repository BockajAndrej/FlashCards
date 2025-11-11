using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashCards.WebBlazor.Bl.Facades;

public class TagWebFacade(ITagApiClient apiClient) : IWebFacade<TagListModel, TagDetailModel>
{
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.TagDELETEAsync(id);
    }

    public async Task<ICollection<TagListModel>> GetAllAsync(string? filterAtrib = null, string? filter = null, string? orderBy = null, bool? sortDesc = null,
        int? pageNumber = null, int? pageSize = null)
    {
        return await apiClient.TagAllAsync(filterAtrib, filter, orderBy, sortDesc, pageNumber, pageSize);
    }

    public async Task<TagDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.TagGETAsync(id);
    }

    public async Task<int> GetCountAsync(string? strFilterAtrib = null, string? strFilter = null)
    {
        return await apiClient.Count4Async(strFilterAtrib, strFilter);
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