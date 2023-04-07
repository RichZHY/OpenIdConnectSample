using IdentityModel.Client;

namespace OpenIdSample.WebAPI.Client.Services
{
    public class IdentityServer4Service : IIdentityServer4Service
    {
        private DiscoveryDocumentResponse _discoverDocument { get; set; }
        public IdentityServer4Service()
        {
            using (var client = new HttpClient())
            {
                _discoverDocument = client.GetDiscoveryDocumentAsync("https://localhost:7037/.well-known/openid-configuration").Result;
            }
        }
        public async Task<TokenResponse> GetToken(string apiScope)
        {
            using(var client = new HttpClient())
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _discoverDocument.TokenEndpoint,
                    ClientId = "weatherApi",
                    Scope = apiScope,
                    ClientSecret = "ProCodeGuide"
                });
                if (tokenResponse.IsError)
                {
                    throw new Exception("Token Error");
                }
                return tokenResponse;
            }
        }
    }
}
