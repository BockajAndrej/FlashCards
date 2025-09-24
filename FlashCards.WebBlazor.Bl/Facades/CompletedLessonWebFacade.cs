using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashCards.WebBlazor.Bl.Facades;

public class CompletedLessonWebFacade(ICompletedLessonApiClient apiClient) : IWebFacade<CompletedLessonListModel, CompletedLessonDetailModel>
{
	public async Task DeleteAsync(Guid id)
	{
		await apiClient.CompletedLessonDELETEAsync(id);
	}

	public async Task<ICollection<CompletedLessonListModel>> GetAllAsync(string? filterAtrib = null, string? filter = null, string? orderBy = null, bool? sortDesc = null,
		int? pageNumber = null, int? pageSize = null)
	{
		return await apiClient.CompletedLessonAllAsync(filterAtrib, filter, orderBy, sortDesc, pageNumber, pageSize);
	}

	public async Task<CompletedLessonDetailModel> GetByIdAsync(Guid id)
	{
		return await apiClient.CompletedLessonGETAsync(id);
	}

	public async Task<int> GetCountAsync(string? strFilterAtrib = null, string? strFilter = null)
	{
		return await apiClient.Count3Async(strFilterAtrib, strFilter);
	}

	public async Task<Guid> SaveToApiAsync(CompletedLessonDetailModel data)
	{
		var result = await apiClient.CompletedLessonPOSTAsync(data);
		return (Guid)result.Id!;
	}
}