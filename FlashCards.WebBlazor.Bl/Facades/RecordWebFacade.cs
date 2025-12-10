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
		return await apiClient.RecordAllAsync(queryObject.IsCompletedFilter, queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
	}

	public async Task<RecordDetailModel?> GetLastAsync(Guid collectionId)
	{
		return await apiClient.LastAsync(collectionId);
	}
	
	public async Task<RecordDetailModel?> GetActiveAsync(Guid collectionId)
	{
		return await apiClient.ActiveAsync(collectionId);
	}
	
	public async Task<RecordDetailModel> GetByIdAsync(Guid id)
	{
		return await apiClient.RecordGETAsync(id);
	}

	public async Task<int> GetCountAsync(RecordQueryObject queryObject)
	{
		return await apiClient.Count5Async(queryObject.IsCompletedFilter, queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
	}

	public async Task<RecordDetailModel> StartNewGameAsync(RecordDetailModel? data, Guid collectionId)
	{
		return await apiClient.StartNewGameAsync(collectionId, data);
	}

	public async Task<RecordDetailModel> FinishGameAsync(RecordDetailModel? data, Guid collectionId)
	{
		return await apiClient.FinishGameAsync(collectionId,  data);
	}

	public async Task<RecordDetailModel?> SaveToApiAsync(RecordDetailModel? data, Guid collectionId)
	{
		if (data.Id == Guid.Empty)
		{
			var result = await apiClient.StartNewGameAsync(collectionId, data);
			return result;
		}
		await apiClient.RecordPUTAsync(data.Id, data);
		return null;
	}
}