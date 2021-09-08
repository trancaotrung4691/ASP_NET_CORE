using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.Systems.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        public UsersController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.Keyword = keyword;
            var sessions = HttpContext.Session.GetString("Token");
            var request = new GetUserPagingRequest()
            {
                BearerToken = sessions,
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            }; 
            var data = await _userApiClient.GetUserPagings(request);
            return View(data);
        }
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var result = await _userApiClient.RegisterUser(request);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(request);
        }
        #endregion
        #region Update
        [HttpGet()]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _userApiClient.GetUserById(id);
            var userUpdateRequest = new UserUpdateRequest()
            {
                Id = user.Id,
                Dob = user.Dob,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(userUpdateRequest);
        }
        [HttpPost()]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(request);
        }
        #endregion
        #region Detail
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetUserById(id);
            return View(result);
        }
        #endregion
        #region Delete
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var request = new UserDeleteRequest()
            {
                Id = id
            };
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var result = await _userApiClient.DeleteUser(request.Id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(request);
        }
        #endregion
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }
    }
}
