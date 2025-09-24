using IdentityModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace FlashCards.WebBlazor.App.Layout;

public partial class LoginDisplay : ComponentBase
{
    public string UserName = "Anonymous";
    public string UserRole = "None";
    public string UserImage = "images/profile.png";
    
    private bool _isDropdownVisible = false;
    
    protected override async Task OnInitializedAsync()
    {
        AuthenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
        await GetUserInfo();
    }
    private void ToggleDropdown()
    {
        _isDropdownVisible = !_isDropdownVisible;
    }
    public void BeginLogOut()
    {
        Navigation.NavigateToLogout("authentication/logout");
    }
    
    private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        await GetUserInfo();
        StateHasChanged();
    }

    private async Task GetUserInfo()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            if (user.Identity.Name != null) 
                UserName = user.Identity.Name;
            else
                UserName = "Anonymous";
            var userRole = user.FindFirst(JwtClaimTypes.Role);
            if (userRole != null)
                UserRole = userRole.Value;
            else
                UserRole = "None";
            var pictureClaim = user.FindFirst(JwtClaimTypes.Picture);
            if (pictureClaim != null)
                UserImage = pictureClaim.Value;
            else
                UserImage = "images/profile.png";
        }
    }
}