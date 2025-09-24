using Microsoft.AspNetCore.Components;

namespace FlashCards.WebBlazor.App.Components;

public partial class UserProfileCardComponent : ComponentBase
{
    [Parameter]
    public string UserName { get; set; } = "Default User";

    [Parameter]
    public string UserRole { get; set; } = "Default Role";
    
    [Parameter]
    public string ProfileImage { get; set; } = "Images/user.png";
}