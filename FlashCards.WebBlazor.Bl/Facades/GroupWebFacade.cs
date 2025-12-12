using FlashCards.Common.QueryObjects;
using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashCards.WebBlazor.Bl.Facades;

public class GroupWebFacade(IGroupApiClient apiClient) : IWebFacade<GroupQueryObject, GroupListModel, GroupDetailModel>
{
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.GroupDELETEAsync(id);
    }

    public async Task<ICollection<GroupListModel>> GetAllAsync(GroupQueryObject queryObject)
    {
        return await apiClient.GroupAllAsync(queryObject.NameFilter, queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
    }

    public async Task<GroupDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.GroupGETAsync(id);
    }

    public async Task<int> GetCountAsync(GroupQueryObject queryObject)
    {
        return await apiClient.Count5Async(queryObject.NameFilter, queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
    }

    public async Task<GroupDetailModel> CreateGroup(GroupDetailModel data)
    {
        return await apiClient.GroupPOSTAsync(data);
    }
    
    public async Task JoinGroup(GroupDetailModel data)
    {
        await apiClient.JoinToGroupAsync(data);
    }
    
    public async Task LeaveGroup(GroupDetailModel data)
    {
        await apiClient.LeaveFromGroupAsync(data);
    }

    public async Task<Guid> SaveToApiAsync(GroupDetailModel data)
    {
        if (data.Id == Guid.Empty)
        {
            var result = await apiClient.GroupPOSTAsync(data);
            return result.Id;
        }
        await apiClient.GroupPUTAsync(data.Id, data);
        return Guid.Empty;
    }
}