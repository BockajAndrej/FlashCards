using FlashCards.Common.QueryObjects;
using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashCards.WebBlazor.Bl.Facades;

public class CollectionWebFacade(ICollectionApiClient apiClient) : IWebFacade<CollectionQueryObject, CollectionListModel, CollectionDetailModel>
{
	public async Task DeleteAsync(Guid id)
	{
		await apiClient.CollectionDELETEAsync(id);
	}

	public async Task<ICollection<CollectionListModel>> GetAllAsync(CollectionQueryObject queryObject)
	{
		return await apiClient.CollectionAllAsync(queryObject.CreatedByIdFilter, queryObject.NameFilter, queryObject.VisibilityFilter, queryObject.TagIdsFilter , queryObject.RecentOrder, queryObject.NameOrder, queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
	}

	public async Task<CollectionDetailModel> GetByIdAsync(Guid id)
	{
		return await apiClient.CollectionGETAsync(id);
	}

	public async Task<int> GetCountAsync(CollectionQueryObject queryObject)
	{
		return await apiClient.Count3Async(queryObject.CreatedByIdFilter, queryObject.NameFilter, queryObject.VisibilityFilter, queryObject.TagIdsFilter , queryObject.RecentOrder, queryObject.NameOrder, queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
	}

	public async Task<Guid> SaveToApiAsync(CollectionDetailModel data)
	{
		if (data.Id == Guid.Empty)
		{
			var result = await apiClient.CollectionPOSTAsync(data);
			return result.Id;
		}
		await apiClient.CollectionPUTAsync(data.Id, data);
		return Guid.Empty;
	}
}