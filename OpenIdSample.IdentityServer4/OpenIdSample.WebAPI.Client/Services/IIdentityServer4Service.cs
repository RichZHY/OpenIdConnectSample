using IdentityModel.Client;

namespace OpenIdSample.WebAPI.Client.Services
{
    public interface IIdentityServer4Service
    {
        Task<TokenResponse> GetToken(string apiScope);
    }
}
