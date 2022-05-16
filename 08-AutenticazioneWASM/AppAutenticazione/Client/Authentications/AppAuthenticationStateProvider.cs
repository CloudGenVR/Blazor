using AppAutenticazione.Client.Authentications.Abstractions;
using AppAutenticazione.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace AppAutenticazione.Client.Authentications
{
    public class AppAuthenticationStateProvider :
        AuthenticationStateProvider,
        IHostEnvironmentAuthenticationStateProvider,
        IAuthenticationService
    {
        private readonly HttpClient client;
        private readonly ILocalStorageService localStorage;
        private const string LoginKey = "LoginKey";
        private Task<AuthenticationState> _authenticationStateTask;
        private static AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public AppAuthenticationStateProvider(HttpClient client, ILocalStorageService localStorage)
        {
            this.client = client;
            this.localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsync<string>(LoginKey);
            if (!IsTokenValid(token))
            {
                client.DefaultRequestHeaders.Authorization = null;
                await localStorage.RemoveItemAsync(LoginKey);
                return Anonymous;
            }
            var authenticateState = BuildAuthenticationState(token);
            return authenticateState;
        }

        public async Task<LoginUserResponseViewModel> Login(LoginViewModel model)
        {
            var response = await client.PostAsJsonAsync<LoginViewModel>("account/login", model);
            if (response.IsSuccessStatusCode)
            {
                var registerResponse = await response.Content.ReadFromJsonAsync<LoginUserResponseViewModel>();
                if (registerResponse.IsSuccess)
                {
                    await localStorage.SetItemAsync(LoginKey, registerResponse.JwtToken);
                    var authenticationState = BuildAuthenticationState(registerResponse.JwtToken);
                    SetAuthenticationState(Task.FromResult(authenticationState));
                }
                return registerResponse;
            }
            return new LoginUserResponseViewModel
            {
                IsSuccess = false,
            };
        }

        public void SetAuthenticationState(Task<AuthenticationState> authenticationStateTask)
        {
            _authenticationStateTask = authenticationStateTask ?? throw new ArgumentNullException(nameof(authenticationStateTask));
            NotifyAuthenticationStateChanged(_authenticationStateTask);
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ClaimsFromJwt(token), "jwt")));
        }

        private IEnumerable<Claim> ClaimsFromJwt(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                return jwttoken.Claims;
            }
            catch
            {
                return null;
            }
        }

        private bool IsTokenValid(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }

                var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                return jwttoken.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }
    }
}
