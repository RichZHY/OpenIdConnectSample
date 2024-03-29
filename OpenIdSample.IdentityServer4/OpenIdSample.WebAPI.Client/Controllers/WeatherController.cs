﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenIdSample.WebAPI.Client.Models;
using OpenIdSample.WebAPI.Client.Services;

namespace OpenIdSample.WebAPI.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IIdentityServer4Service? _identityServer4Service = null;
        public WeatherController(IIdentityServer4Service identityServer4Service)
        {
            _identityServer4Service = identityServer4Service;
        }
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var OAuth2Token = await _identityServer4Service.GetToken("weatherApi.read");
            using (var client = new HttpClient())
            {
                client.SetBearerToken(OAuth2Token.AccessToken);
                var result = await client.GetAsync("https://localhost:7100/weatherforecast");
                if (result.IsSuccessStatusCode)
                {
                    var model = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<WeatherForecast>>(model);
                }
                else
                {
                    throw new Exception("Some Error while fetching Data");
                }
            }
        }
    }
}
