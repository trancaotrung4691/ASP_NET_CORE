using eShopSolution.Application.Systems.Users;
using eShopSolution.ViewModels.Systems.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var loginToken = await _userService.Authenticate(loginRequest);
            if (string.IsNullOrEmpty(loginToken))
            {
                return BadRequest("User name or password is incorrect");
            }
            return Ok(loginToken);
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isRegisterSuccessful = await _userService.Register(registerRequest);
            if (!isRegisterSuccessful)
            {
                return BadRequest("Register is unsuccessful");
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isRegisterSuccessful = await _userService.Update(id, request);
            if (!isRegisterSuccessful)
            {
                return BadRequest("Update is unsuccessful");
            }
            return Ok();
        }

        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var users = await _userService.GetUserPaging(request);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.Delete(id);
            return Ok(result);
        }
    }
}
