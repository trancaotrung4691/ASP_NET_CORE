using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Systems.Users;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var client = GetHttpClient();
            var jsonRequest = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var reponse = await client.PostAsync("/api/users/authenticate", httpContent);
            var token = await reponse.Content.ReadAsStringAsync();
            return token;
        }

        public async Task<PageResult<UserViewModel>> GetUserPagings(GetUserPagingRequest request)
        {
            var client = GetHttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.BearerToken);
            var response = await client.GetAsync($"/api/users/paging?pageIndex=" +
                                                $"{request.PageIndex}&pageSize={request.PageSize}" +
                                                $"&keyword={request.Keyword}");
            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<PageResult<UserViewModel>>(body);
            return users;
        }

        public async Task<bool> RegisterUser(RegisterRequest request)
        {
            var client = GetHttpClient();
            var jsonRequest = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/users", httpContent);
            return response.IsSuccessStatusCode;
        }

        private HttpClient GetHttpClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            return client;
        }
    }
}
