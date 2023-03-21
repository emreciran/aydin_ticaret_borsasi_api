﻿using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ViewModels;

namespace aydin_ticaret_borsasi_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _authRepository;
        IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authRepository.RegisterUser(model);

                if (result.IsSuccess) return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authRepository.LoginUser(model);

                if (result.IsSuccess)
                {
                    var jwt = result.AuthResult.Token;

                    HttpContext.Response.Cookies.Append("jwt", jwt, new CookieOptions
                    {
                        Secure = true,
                        HttpOnly = false,
                        Expires = DateTime.Now.AddDays(1),
                        SameSite = SameSiteMode.None,
                        IsEssential = true,
                    });

                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userid, [FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var result = await _authRepository.ConfirmEmail(userid, token);

            if (result.IsSuccess)
            {
                return Redirect(_configuration["ClientURL"] + "/auth/login?confirmEmail=" + token);
            }

            return BadRequest(result);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest();

            var result = await _authRepository.ForgotPassword(email);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var response = await _authRepository.ResetPassword(model);

            if (response.IsSuccess) return Ok(response);

            return BadRequest(response);
        }
    }
}
