using Course.Shared.Results;
using Course.UI.Models;
using Course.UI.Models.Identity;
using Course.UI.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Course.UI.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient client, IHttpContextAccessor httpContextAccessor, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = client;
            _httpContextAccessor = httpContextAccessor;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public async Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
           
            _httpClient.BaseAddress = new Uri(_serviceApiSettings.IdentityBaseUri);
            var request = await _httpClient.PostAsync("api/Auth/refresh-token", null).ConfigureAwait(false);
            var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            var token = JsonConvert.DeserializeObject<TokenResponse>(content);


            var authenticationTokens = new List<AuthenticationToken>()
            {
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.AccessToken,Value=token.Token},
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.RefreshToken,Value=token.RefleshToken},
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.ExpiresIn,Value= token.Expiration.ToString("o",CultureInfo.InvariantCulture)}
            };

            var authenticationResult = await _httpContextAccessor.HttpContext.AuthenticateAsync();

            var properties = authenticationResult.Properties;
            properties.StoreTokens(authenticationTokens);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticationResult.Principal, properties);

            return token;
        }

    


        public async Task<IResult> RevokeRefreshToken()
        {
            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            var postRequest = new StringContent(JsonConvert.SerializeObject(new  { token = refreshToken }), Encoding.UTF8, "application/json");
          
            _httpClient.BaseAddress = new Uri(_serviceApiSettings.IdentityBaseUri);
            var request = await _httpClient.PostAsync("api/Auth/revoke-token", postRequest).ConfigureAwait(false);
            var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            
            
            var response = JsonConvert.DeserializeObject<RevokeRefreshTokenResponseModel>(content.ToString());

            if (response.IsSuccess)
            {         
                
                return new SuccessResult(response.Message);
            }
           
            return new ErrorResult(response.Message); 

        }
 

        public async Task<IResult> SignIn(SignInModel signinInput)
        {
            var postRequest = new StringContent(JsonConvert.SerializeObject(signinInput), Encoding.UTF8, "application/json");
            _httpClient.BaseAddress = new Uri(_serviceApiSettings.IdentityBaseUri);
            var request = await _httpClient.PostAsync("api/Auth/login", postRequest).ConfigureAwait(false);
            var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            var token = JsonConvert.DeserializeObject<TokenResponse>(content);
  

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authenticationProperties = new AuthenticationProperties();

            authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.AccessToken,Value=token.Token},
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.RefreshToken,Value=token.RefleshToken},
                new AuthenticationToken{ Name=OpenIdConnectParameterNames.ExpiresIn,Value= token.Expiration.ToString("o",CultureInfo.InvariantCulture)}
            });

            authenticationProperties.IsPersistent = signinInput.IsRemember;

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);
            
            return new SuccessResult();


        }
    }
}
