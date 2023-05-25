using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeFin.Models;


namespace WeFin.Services
{/*
    internal class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(UserLogin rqtDto)
        {
            var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new(nameof(UserLogin.UserName), rqtDto.UserName),
                new(nameof(UserLogin.UserPassword), rqtDto.UserPassword),
            });
            using var rsp = await _httpClient.PostAsync("/account/login", content);
            if (!rsp.IsSuccessStatusCode)
            {
                return false;
            }
            var authToken = await rsp.Content.ReadAsStringAsync();
            await _localStorage.SetItemAsync("authToken", authToken);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(rqtDto.Account);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
    */
}
