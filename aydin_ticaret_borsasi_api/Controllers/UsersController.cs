using BusinessLayer.Abstract;
using EntitiesLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aydin_ticaret_borsasi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var updatedUser = await _userService.UpdateUser(user);
            if (updatedUser == null) return BadRequest();

            return Ok(updatedUser);
        }
    }
}
