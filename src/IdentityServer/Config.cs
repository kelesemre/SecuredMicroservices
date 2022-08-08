using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "movieClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256()) // This ensures that the request to get the access token is made only from the application, and not from a potential attacker that may have intercepted the authorization code
                    },
                    AllowedScopes = { "movieAPI" }
                },
                new Client
                {
                    ClientId = "movies_mvc_client",
                    ClientName = "Movies MVC Web App",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:5002/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:5002/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256()) // This ensures that the request to get the access token is made only from the application, and not from a potential attacker that may have intercepted the authorization code
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "movieAPI",
                        "roles" // we can carry the scope named "roles" information from IDS to our client application
                    }
                } 
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("movieAPI", "Movie API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               new ApiResource("movieAPI", "Movie API")
          };

        public static IEnumerable<IdentityResource> IdentityResources => // a named group of claims that can be requested using the scope parameter.
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource( "roles", "Your role(s)", new List<string>() { "role" })
          };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "Emre",
                    Password = "pass",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "emre"),
                        new Claim(JwtClaimTypes.FamilyName, "keles"),
                        new Claim(JwtClaimTypes.Role, "admin") // added
                    }
                },
                 new TestUser
                {
                    SubjectId = "6BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "adm",
                    Password = "pass",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "adm"),
                        new Claim(JwtClaimTypes.FamilyName, "keles"),
                        new Claim(JwtClaimTypes.Role, "user") //added
                    }
                }
            };
    }
}
