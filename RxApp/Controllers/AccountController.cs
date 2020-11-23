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
using RxApp.Data;

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
        private readonly IUnitOfWork _uow;


        public AccountController(IUnitOfWork uow,
            IMapper mapper, 
            UserManager<Customer> userManager, 
            IConfiguration config, 
            SignInManager<Customer> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
            _uow = uow;
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

        [HttpPost("Allergenes/{id}")]
        public async Task<IActionResult> AddPacientAllergenes(string id, IEnumerable<int> inredientIds) {

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest("No user with such id");
            }

            var allergenesToRemove = _uow.AllergyRepository.Get(s => s.CustomerId == id);

            _uow.AllergyRepository.RemoveRange(allergenesToRemove);

            foreach (var i in inredientIds) {
                var ingredient = _uow.ActiveIngredientRepository.Get(s => s.Id == i).FirstOrDefault();

                if (ingredient == null) {
                    return BadRequest("No such inredient with id " + ingredient.ToString());
                }

                var allergy = new Allergy {
                    ActiveIngredientId = i,
                    CustomerId = id
                };

                _uow.AllergyRepository.Add(allergy);
            }



            if (_uow.Complete()) {
                return Ok();
            }

            return BadRequest("Error during adding allergenes");

            
        }

        [HttpGet("Allergenes/{id}")]
        public async Task<ActionResult> PacientAllergenes(string id) {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest("No user with such id");
            }

            var allergenes = _uow.AllergyRepository.Get(s => s.CustomerId == id).FirstOrDefault();

            return Ok(allergenes);
        }


        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetUser(string id) {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) {
                return BadRequest("No user with such id");
            }

            return Ok(user);
        }



    }
}
