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
    [Authorize(Roles = "Admin, Pharmacist, Medic")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("Roles")]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("Users")]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return Ok(users);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Roles/{name}")]
        public async Task<IActionResult> GetUsersInRole(string name) {

            var role = await _roleManager.FindByNameAsync(name);

            if (role == null) {
                return BadRequest("No such role");
            }
            IEnumerable<string> emailsOfUsers = (await _userManager.GetUsersInRoleAsync(name)).Select(u => u.Email);

            return Ok(emailsOfUsers); 
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Roles/{roleName}")]
        public async Task<IActionResult> EditUsersInRole(string roleName, IEnumerable<string> model) {

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                return BadRequest("No such role");
            }

            foreach(var email in model)
            {
                var user = await _userManager.FindByEmailAsync(email); 
                var userRole = (await _userManager.GetRolesAsync(user))[0];

                if (userRole != roleName) {
                    var result = await _userManager.RemoveFromRoleAsync(user, userRole);
                    if (!result.Succeeded) 
                    {
                        return BadRequest("Error while removeing from role");
                    }
                    result = await _userManager.AddToRoleAsync(user, roleName);
                }
                continue;
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("Ingredients/{name}")]
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
        [Authorize(Roles = "Admin")]
        [HttpPut("Ingredients/{id}/{model}")]
        public ActionResult EditIngredient(int id, string model)
        {
            var ingredientFromDb = _uow.ActiveIngredientRepository.Get(s => s.Id == id).FirstOrDefault();

            var existingIngredient = _uow.ActiveIngredientRepository.Get(s => s.Name == model)
                .FirstOrDefault();
            if (existingIngredient != null)
            {
                return BadRequest("Such ingredient already exists");
            }
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

        [HttpGet("PharmGroups")]
        public ActionResult GetAllPharmGroups()
        {
            var PharmGroupFromDb = _uow.PharmGroupRepository.GetAll();

            if (PharmGroupFromDb.Count() > 0)
            {
                return Ok(PharmGroupFromDb);
            }
            return BadRequest("No pharm groups");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("PharmGroups/{name}")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost("PharmGroups/{model}")]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("PharmGroups/{id}")]
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

        [Authorize(Roles = "Admin")]
        [HttpPut("PharmGroups/{id}/{model}")]
        public ActionResult EditPharmGroup(int id, string model)
        {


            var PharmGroupFromDb = _uow.PharmGroupRepository.Get(s => s.Id == id).FirstOrDefault();

            var existingPharmGroup = _uow.PharmGroupRepository.Get(s => s.Name == model)
                .FirstOrDefault();

            if (existingPharmGroup != null)
            {
                return BadRequest("Such pharm group already exists");
            }
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

        [HttpGet("Incompatible")]
        public ActionResult GetAllIncompatibleIngredients()
        {
            var IncompatibleIngredientFromDb = _uow.IncompatibleIngredientRepository.GetAll();

            IEnumerable<IncompatibleDto> model = new List<IncompatibleDto>();

            foreach (var i in IncompatibleIngredientFromDb) {
                var firstName = _uow.ActiveIngredientRepository
                    .Find(s => s.Id == i.FirstIngredientId)
                    .FirstOrDefault()
                    .Name;
                var secondName = _uow.ActiveIngredientRepository
                    .Find(s => s.Id == i.SecondIngredientId)
                    .FirstOrDefault()
                    .Name;

                var incompatible = new IncompatibleDto
                {
                    NameFirst = firstName,
                    NameSecond = secondName,
                    IdFirst = i.FirstIngredientId,
                    IdSecond = i.SecondIngredientId,
                    IncompatibleId = i.Id
                };
                model = model.Append(incompatible);
            }

            return Ok(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Incompatible/{firstId}/{secondId}")]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("Incompatible/{id}")]
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
