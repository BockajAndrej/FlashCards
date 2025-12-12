using FlashCards.Common.QueryObjects;
using FlashCards.WebBlazor.Bl.ApiClient;
using FlashCards.WebBlazor.Bl.Facades.Interfaces;

namespace FlashUsers.WebBlazor.Bl.Facades;

public class UserWebFacade(IUserApiClient apiClient) : IWebFacade<UserQueryObject, UserListModel, UserDetailModel>
{
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.UserDELETEAsync(id);
    }

    public async Task<ICollection<UserListModel>> GetAllAsync(UserQueryObject queryObject)
    {
        return await apiClient.UserAllAsync(queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
    }

    public async Task<UserDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.UserGETAsync(id);
    }

    public async Task<UserDetailModel> GetActualUserAsync()
    {
        return await apiClient.GetActualUserAsync();
    }
    
    public async Task<int> GetCountAsync(UserQueryObject queryObject)
    {
        return await apiClient.Count8Async(queryObject.IsDescending, queryObject.PageNumber, queryObject.PageSize);
    }

    public async Task<UserDetailModel> CreateUser(UserDetailModel userDetailModel)
    {
        return await apiClient.CreateUserAsync(userDetailModel);
    }

    public async Task<Guid> SaveToApiAsync(UserDetailModel data)
    {
        if (data.Id == Guid.Empty)
        {
            var result = await apiClient.UserPOSTAsync(data);
            return result.Id;
        }

        await apiClient.UserPUTAsync(data.Id, data);
        return Guid.Empty;
    }
}