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
                ClientId = "FlashCardClientId",
                ClientName = "TaskManagerClientName",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                AllowedGrantTypes = new List<string>
                    {
                        GrantType.ClientCredentials,
                        GrantType.ResourceOwnerPassword,
                        GrantType.AuthorizationCode,
                    },
                
                RedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                // PostLogoutRedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                // FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                // PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "FlashCardsApiScope" }
            },
        };
}