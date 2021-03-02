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
            try {
                var user = model.UserName.Contains('@')
                    ? await _userManager.FindByEmailAsync(model.UserName)
                    : await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    return BadRequest("Verifique su nombre de usuario o correo");
                }

                var result = await _signInManager.PasswordSignInAsync(
                    user, model.Password, model.RememberMe, false);

                if (!result.Succeeded)
                {
                    return BadRequest("Verifique su contraseña");
                }

                return BuildToken(user);

            } catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
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
                return BadRequest(e.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(UserModel model)
        {
            try
            {
                var user = model.UserName.Contains('@')
                    ? await _userManager.FindByEmailAsync(model.UserName)
                    : await _userManager.FindByNameAsync(model.UserName);

                var result = await _userManager.ChangePasswordAsync(
                    user, model.Password, model.NewPassword);

                if (!result.Succeeded)
                {
                    return BadRequest("Verifique su contraseña anterior");
                }

                await _signInManager.SignInAsync(user, false);

                return BuildToken(user);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
                return BadRequest(e.Message);
            }

        }

        [NonAction]
        public IActionResult BuildToken(UserEntity user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
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
