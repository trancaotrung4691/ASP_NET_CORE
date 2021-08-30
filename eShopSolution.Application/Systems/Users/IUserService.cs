using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Systems.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Systems.Users
{
    public interface IUserService
    {
        Task<string> Authenticate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
        Task<PageResult<UserViewModel>> GetUserPaging(GetUserPagingRequest request);
    }
}
