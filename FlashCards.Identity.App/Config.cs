using Duende.IdentityServer.Models;

namespace FlashCards.Identity.App;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new("FlashCardsApiScope")
            {
                UserClaims = new[] {"role"}
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // interactive client using code flow + pkce
            new()
            {
                ClientId = "PostmanClientId",
                ClientName = "Postman Client",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                
                RedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                PostLogoutRedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                // FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                // PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "FlashCardsApiScope" }
            },
            new()
            {
                ClientId = "FlashCardClientId",
                ClientName = "TaskManagerClientName",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                
                RedirectUris = { "https://localhost:7046/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:7046" },
                // FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                // PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },
                
                AllowedCorsOrigins = { "https://localhost:7046" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "FlashCardsApiScope" }
            },
        };
}