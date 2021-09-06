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
        Task<bool> RegisterUser(RegisterRequest request);
        Task<bool> UpdateUser(Guid id, UserUpdateRequest request);
        Task<UserViewModel> GetUserById(Guid id);
        Task<bool> DeleteUser(Guid id);
    }
}
