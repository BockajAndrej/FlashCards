using FlashCards.Common.QueryObjects;
using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashCards.WebBlazor.Bl.Facades;

public class RecordWebFacade(IRecordApiClient apiClient) : IWebFacade<RecordQueryObject, RecordListModel, RecordDetailModel>
{
	public async Task DeleteAsync(Guid id)
	{
		await apiClient.RecordDELETEAsync(id);
	}

	public async Task<ICollection<RecordListModel>> GetAllAsync(RecordQueryObject queryObject)
	{
		return await apiClient.RecordAllAsync(queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
	}

	public async Task<RecordDetailModel> GetByIdAsync(Guid id)
	{
		return await apiClient.RecordGETAsync(id);
	}

	public async Task<int> GetCountAsync(RecordQueryObject queryObject)
	{
		return await apiClient.Count4Async(queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
	}

	public async Task<Guid> SaveToApiAsync(RecordDetailModel data)
	{
		if (data.Id == Guid.Empty)
		{
			var result = await apiClient.RecordPOSTAsync(data);
			return result.Id;
		}
		await apiClient.RecordPUTAsync(data.Id, data);
		return Guid.Empty;
	}
}