using AppAutenticazione.Server.Models;
using AppAutenticazione.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppAutenticazione.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            var isAuthenticated = await userService.LoginAsync(loginUser.Email, loginUser.Password);
            if (!isAuthenticated)
                return Unauthorized(new LoginUserResponseViewModel { IsSuccess = false });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginUser.Email),
                new Claim(ClaimTypes.Email, loginUser.Email),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "UserTradizionale"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
               configuration["Jwt:Issuer"],
               configuration["Jwt:Audience"],
               claims,
               expires: DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:ExpiringInMinutes"])),
               signingCredentials: creds
            );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return Ok(new LoginUserResponseViewModel
            {
                IsSuccess = true,
                JwtToken = token,
                UserNotExist = true
            });
        }
    }
}
