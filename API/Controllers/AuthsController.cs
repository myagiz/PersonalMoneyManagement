using Business.Abstract;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IUserService _userService;

        private readonly ITokenService _tokenService;

        public AuthsController(IUserService userService, IConfiguration configuration, ITokenService tokenService)
        {
            _userService = userService;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpGet("Welcome")]
        public IActionResult Welcome()
        {
            return Ok("Welcome");
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterDto model)
        {
            var result=await _userService.RegisterAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var getUser = _userService.GetUserForLoginAsync(model).Result;
            if (getUser.Success)
            {
                TokenService tokenService = new TokenService(_configuration);
                Token token = _tokenService.CreateAccessToken(getUser.Data);
                await _userService.GenerateUserRefreshToken(getUser.Data.Id, token.RefreshToken, DateTime.Now, token.Expiration);
                return Ok(token);
            }

            return BadRequest(getUser);
        }

    }
}
