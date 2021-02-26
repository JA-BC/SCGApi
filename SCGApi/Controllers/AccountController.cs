using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SCG.Core.Database.Entities;
using SCG.Core.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SCGApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserModel model)
        {
            var context = HttpContext;
            var result = await _signInManager.PasswordSignInAsync(
                model.UserName, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                return BadRequest("Usuario o contraseña incorrectos");
            }

            return BuildToken(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserModel model)
        {
            try
            {
                var user = new UserEntity
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(result.Errors);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [NonAction]
        public IActionResult BuildToken(UserModel model)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, model.UserName),
                //new Claim(JwtRegisteredClaimNames.Email, model.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret-key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Authentication:Expires"])),
                signingCredentials: creds);

            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
