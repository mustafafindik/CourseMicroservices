using Course.Services.Identity.Dtos;
using Course.Services.Identity.Models;
using Course.Services.Identity.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
      
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userToLogin =await _authService.Login(loginDto);
            if (!userToLogin.IsSuccess)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data,ipAddress());
            if (result.IsSuccess)
            {
                setTokenCookie(result.Data.RefleshToken);
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(SignupDto signupDto)
        {
            var userExists = await _authService.UserExists(signupDto.Email);
            if (!userExists.IsSuccess)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = await _authService.SignUp(signupDto);
            var result = _authService.CreateAccessToken(registerResult.Data,ipAddress());
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _authService.RefreshToken(refreshToken, ipAddress());
            if (response.IsSuccess)
            {
                setTokenCookie(response.Data.RefleshToken);
                return Ok(response.Data);
            }

            return BadRequest(response.Message);

        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response =await _authService.RevokeToken(token, ipAddress());

            if (!response.IsSuccess)
                return NotFound(response);

            return Ok(response);
        }





        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }


}