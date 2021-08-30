using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Systems.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
        Task<PageResult<UserViewModel>> GetUserPagings(GetUserPagingRequest request);
    }
}
