using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AnimalShelter.Models;
using Microsoft.Extensions.Logging;

namespace AnimalShelter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;

            var jwtSecret = _configuration["JWT:Secret"];
            var jwtValidIssuer = _configuration["JWT:ValidIssuer"];
            var jwtValidAudience = _configuration["JWT:ValidAudience"];

            _logger.LogInformation($"JWT:Secret = {jwtSecret}");
            _logger.LogInformation($"JWT:ValidIssuer = {jwtValidIssuer}");
            _logger.LogInformation($"JWT:ValidAudience = {jwtValidAudience}");
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExists = await _userManager.FindByEmailAsync(user.Email);
            if (userExists != null)
            {
                return BadRequest(new { status = "error", message = "Email already exists" });
            }

            var newUser = new ApplicationUser() { Email = user.Email, UserName = user.UserName };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                return Ok(new { status = "success", message = "User has been successfully created" });
            }
            else
            {
                return BadRequest(new { status = "error", message = "Failed to create user", errors = result.Errors });
            }
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SignIn(SignInDto userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(userInfo.Email);
            if (user == null)
            {
                return BadRequest(new { status = "error", message = "Invalid credentials" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userInfo.Password, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { status = "success", message = "User signed in successfully", token });
            }

            return BadRequest(new { status = "error", message = "Invalid credentials" });
        }
    }
}