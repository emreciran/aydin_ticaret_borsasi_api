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
            if(users == null) return BadRequest();

            return Ok(users);
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
    }
}
