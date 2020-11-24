using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RxApp.Data;
using RxApp.Models;
using RxApp.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RxApp.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<Customer> _userManager;
        private readonly IMapper _mapper;


        public AdministrationController(IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager,
            UserManager<Customer> userManager,
            IMapper mapper)
        {
            _uow = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("Roles")]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }

        [HttpGet("Roles/{id}")]
        public async Task<IActionResult> GetUsersInRole(string id) {

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) {
                return BadRequest("No such role");
            }

            List<string> emailsOfUsers = new List<string>();

            foreach (var user in _userManager.Users) {
                if (await _userManager.IsInRoleAsync(user, role.Name)) {
                    emailsOfUsers.Add(user.Email);
                }
            }

            return Ok(emailsOfUsers); 
        }

        [HttpPost("Roles/{id}")]
        public async Task<IActionResult> EditUsersInRole(string id, EditUsersInRole model) {

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return BadRequest("No such role");
            }

            var users = model.Users;

            foreach(var user in users)
            {
                var userFromDb = await _userManager.FindByEmailAsync(user.Email);

                IdentityResult result = null;
                if (user.IsSelected && !(await _userManager.IsInRoleAsync(userFromDb, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(userFromDb, role.Name);
                }
                else if (!user.IsSelected && await _userManager.IsInRoleAsync(userFromDb, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(userFromDb, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    continue;
                }
                else {
                    return BadRequest(result.Errors);
                }

            }

            return Ok();
        }


        [HttpGet("Ingredients")]
        public ActionResult GetAllIngredients()
        {
            var ingredientsFromDb = _uow.ActiveIngredientRepository.GetAll();

            return Ok(ingredientsFromDb);
                


        }

        [HttpGet("Ingredients/{name}")]
        public ActionResult GetIngredient(string name)
        {
            var ingredient = _uow.ActiveIngredientRepository.Get(s => s.Name == name)
                .FirstOrDefault();

            if (ingredient == null) {
                return BadRequest("No such Ingredient");
            }


            return Ok(ingredient);
        }

        [HttpPost("Ingredients/{model}")]
        public ActionResult CreateIngredient(string model) {
            var ingredientFromDb = _uow.ActiveIngredientRepository.Get(s => s.Name == model)
                .FirstOrDefault();
            if (ingredientFromDb != null) {
                return BadRequest("Such ingredient already exists");
            }

            var ingredient = new ActiveIngredient { Name = model};
            _uow.ActiveIngredientRepository.Add(ingredient);

            if (_uow.Complete()) {
                return Ok();
            }

            return BadRequest("Problem creating Ingredient");

        }

        [HttpDelete("Ingredients/{id}")]
        public ActionResult DeleteIngredient(int id)
        {
            var ingredientFromDb = _uow.ActiveIngredientRepository
                .Get(s => s.Id == id)
                .FirstOrDefault();

            if (ingredientFromDb != null)
            {
                var incompatibleFirst = _uow.IncompatibleIngredientRepository
                    .Get(s => s.FirstIngredientId == id);
                var incompatibleSecond = _uow.IncompatibleIngredientRepository
                    .Get(s => s.SecondIngredientId == id);

                var incompatible = incompatibleFirst.Concat(incompatibleSecond);


                _uow.IncompatibleIngredientRepository.RemoveRange(incompatible);

                _uow.ActiveIngredientRepository.Remove(ingredientFromDb);
            }

            if (_uow.Complete())
            {
                return Ok();
            }

            return BadRequest("Problem deleting Ingredient");

        }

        [HttpPut("Ingredients/{id}/{model}")]
        public ActionResult EditIngredient(int id, string model)
        {
            var ingredientFromDb = _uow.ActiveIngredientRepository.Get(s => s.Id == id).FirstOrDefault();
            if (ingredientFromDb != null)
            {
                ingredientFromDb.Name = model;
                
            }

            if (_uow.Complete())
            {
                return Ok();
            }

            return BadRequest("Problem deleting Ingredient");

        }

        [HttpGet("PharmGroup")]
        public ActionResult GetAllPharmGroups()
        {
            var PharmGroupFromDb = _uow.PharmGroupRepository.GetAll();

            var PharmGroup = _mapper.Map<IEnumerable<PharmGroupDto>>(PharmGroupFromDb); //  LOOK

            return Ok(PharmGroup);



        }

        [HttpGet("PharmGroup/{name}")]
        public ActionResult GetPharmGroup(string name)
        {
            var PharmGroup = _uow.PharmGroupRepository.Get(s => s.Name == name)
                .FirstOrDefault();

            if (PharmGroup == null)
            {
                return BadRequest("No such PharmGroup");
            }


            return Ok(PharmGroup);

        }

        [HttpPost("PharmGroup/{model}")]
        public ActionResult CreatePharmGroup(string model)
        {
            var PharmGroupFromDb = _uow.PharmGroupRepository.Get(s => s.Name == model)
                .FirstOrDefault();

            if (PharmGroupFromDb != null) {
                return BadRequest("Such pharm group already exists");
            }

            var pharmGroup = new PharmGroup { Name = model};
            _uow.PharmGroupRepository.Add(pharmGroup);
            

            if (_uow.Complete())
            {
                return Ok();
            }

            return BadRequest("Problem creating PharmGroup");

        }

        [HttpDelete("PharmGroup/{id}")]
        public ActionResult DeletePharmGroup(int id)
        {
            var PharmGroupFromDb = _uow.PharmGroupRepository.Get(s => s.Id == id).FirstOrDefault();
            if (PharmGroupFromDb != null)
            {
                _uow.PharmGroupRepository.Remove(PharmGroupFromDb);
            }

            if (_uow.Complete())
            {
                return Ok();
            }

            return BadRequest("Problem deleting PharmGroup");

        }

        [HttpPut("PharmGroup/{id}/{model}")]
        public ActionResult EditPharmGroup(int id, string model)
        {
            var PharmGroupFromDb = _uow.PharmGroupRepository.Get(s => s.Id == id).FirstOrDefault();
            if (PharmGroupFromDb != null)
            {
                PharmGroupFromDb.Name = model;
            }
            else {
                return BadRequest("No such pharmGroup");
            }

            if (_uow.Complete())
            {
                return Ok();
            }

            return BadRequest("Problem deleting PharmGroup");

        }


        [HttpGet("Incompatible-Ingredient")]
        public ActionResult GetAllIncompatibleIngredients()
        {
            var IncompatibleIngredientFromDb = _uow.IncompatibleIngredientRepository.GetAll();

            return Ok(IncompatibleIngredientFromDb);
        }

        [HttpPost("Incompatible-Ingredient/{firstId}/{secondId}")]
        public ActionResult CreateIncompatibleIngredient(int firstId, int secondId)
        {

            var ingredient1 = _uow.ActiveIngredientRepository
                .Get(s => s.Id == firstId)
                .FirstOrDefault();

            var ingredient2 = _uow.ActiveIngredientRepository
                .Get(s => s.Id == secondId)
                .FirstOrDefault();

            if (ingredient1 == null || ingredient2 == null) {
                return BadRequest("No such ingredient");
            }

            var incompatibelFromDb = _uow.IncompatibleIngredientRepository
                .Get(s => s.FirstIngredientId == firstId)
                .Where(s => s.SecondIngredientId == secondId)
                .FirstOrDefault();

            if (incompatibelFromDb != null) {
                return BadRequest("Such incompatible Inredient already exist");
            }
            var incompatible = new IncompatibleIngredient
            {
                FirstIngredientId = firstId,
                SecondIngredientId = secondId
            };

            _uow.IncompatibleIngredientRepository.Add(incompatible);
            

            if (_uow.Complete())
            {
                return Ok();
            }

            return BadRequest("Problem creating IncompatibleIngredient");

        }

        [HttpDelete("Incompatible-Ingredient/{id}")]
        public ActionResult DeleteIncompatibleIngredient(int id)
        {


            var incompatible = _uow.IncompatibleIngredientRepository
                .Get(s => s.Id == id)
                .FirstOrDefault();

            if (incompatible == null)
            {
                return BadRequest("No such incompatible ingredients ");
            }

            _uow.IncompatibleIngredientRepository.Remove(incompatible);

            if (_uow.Complete())
            {
                return Ok();
            }

            return BadRequest("Problem deleting IncompatibleIngredient");

        }

    }
}
