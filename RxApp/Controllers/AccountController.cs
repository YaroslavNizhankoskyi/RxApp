using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RxApp.Models;
using RxApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using RxApp.Helpers;

namespace RxApp.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private SignInManager<Customer> _signInManager;


        public AccountController(IMapper mapper, UserManager<Customer> userManager, IConfiguration config, SignInManager<Customer> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {

            Customer customer = new Customer {
                UserName = model.Name,
                Email = model.Email,
            };


            if ((await _userManager.FindByEmailAsync(customer.Email) != null))
                return BadRequest("UserName email exists");

            var result = await _userManager.CreateAsync(customer, model.Password);

            if (result.Succeeded) {
                //var autorize_result = await _userManager.AddToRoleAsync(customer, "Pacient");

                /*if (!autorize_result.Succeeded)
                {

                    return BadRequest(autorize_result.Errors);

                }*/
                return Ok();
            }
            return BadRequest("Аn error occured");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null) {
                return NoContent();
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

            if (result.Succeeded)
            {
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token)
                });
            }

            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model) {

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null) {
                return BadRequest("No user with such Email");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword,
                    model.NewPassword);

            if (result.Succeeded) {
                await _signInManager.SignOutAsync();
                return Ok();
            }

            return BadRequest(result.Errors);
        }


        [Authorize]
        [HttpPut("Profile")]
        public async Task<IActionResult> ChangeProfile(ChangeProfileDto model) {

            var user = await _userManager.FindByIdAsync(model.Id);

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return BadRequest("User with such Email already exists");
            }

            var updatedUser = _mapper.Map(model, user);

            var result = await _userManager.UpdateAsync(updatedUser);

            if (result.Succeeded) {
                return Ok();
            }

            return BadRequest("An Error occured during user profile update");
        }

    }
}
