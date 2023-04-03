using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ViewModels;

namespace aydin_ticaret_borsasi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int page, float limit)
        {
            var users = await _userService.GetAllUsers(page, limit);
            if (users == null) return BadRequest();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);  
            if (user == null) return BadRequest();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            var result = await _userService.CreateUser(model);
            if (result == null) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var updatedUser = await _userService.UpdateUser(user);
            if (updatedUser == null) return BadRequest();

            return Ok(updatedUser);
        }

        [HttpPut("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo(UpdateInfoViewModel model)
        {
            var updatedUser = await _userService.UpdateUserInfo(user);
            if (updatedUser == null) return BadRequest();

            return Ok(updatedUser);
        }

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(ChangePasswordViewModel model)
        {
            var response = await _userService.ChangeUserPassword(model);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }
    }
}
