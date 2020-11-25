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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        private readonly UserManager<Customer> _userManager;

        private readonly IMapper _mapper;

        public RecipeController(IUnitOfWork uow, UserManager<Customer> userManager, IMapper mapper)
        {
            _uow = uow;
            _userManager = userManager;
            _mapper = mapper;

        }

        [Authorize(Roles = "Medic, Pharmacist, Patient")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            string slaveId = "";

            if (user == null)
            {
                return BadRequest("No such user");
            }


            IEnumerable<Recipe> recipes = new List<Recipe>();

            if (await _userManager.IsInRoleAsync(user, "Patient"))
            {
                recipes = _uow.RecipeRepository
                    .Get(s => s.PatientId == id)
                    .Where(s => s.IsDeletedForPatient == false);

                if (recipes.Count() == 0)
                {
                    return BadRequest("No recipes");
                }
                slaveId = recipes.FirstOrDefault().MedicId;

            }
            else if (await _userManager.IsInRoleAsync(user, "Medic"))
            {
                recipes = _uow.RecipeRepository
                    .Get(s => s.MedicId == id)
                    .Where(s => s.IsDeletedForMedic == false);

                if (recipes.Count() == 0)
                {
                    return BadRequest("No recipes");
                }
                slaveId = recipes.FirstOrDefault().PatientId;

            }


            IEnumerable<RecipeDto> model = new List<RecipeDto>();
            foreach (var i in recipes)
            {
                var dto = new RecipeDto
                {
                    Id = i.Id,
                    MasterId = id,
                    SlaveId = slaveId,
                    Created = i.Time,
                    Start = i.Start,
                    End = i.End
                };

                model.Append(dto);

            }

            return Ok(model);

        }

        [Authorize(Roles = "Medic")]
        [HttpPost("{medicId}")]
        public async Task<IActionResult> CreateRecipe(string medicId, CreateRecipeDto model)
        {

            var user = await _userManager.FindByIdAsync(medicId);

            if (user == null)
            {
                return BadRequest("No such user");
            }

            if (await _userManager.IsInRoleAsync(user, "Medic"))
            {

                var recipe = new Recipe
                {
                    End = model.End,
                    Start = model.Start,
                    Time = model.Time,
                    MedicId = model.MedicId,
                    PatientId = model.PatientId
                };

                _uow.RecipeRepository.Add(recipe);

            }

            return BadRequest();
        }


        [Authorize(Roles = "Medic")]
        [HttpPost("recipe/{recipeId}")]
        public async Task<IActionResult> CreateRecipeDrug(int recipeId, RecipeDrugDto model)
        {

            var recipeDrug = _mapper.Map<RecipeDrug>(model);

            recipeDrug.IsSold = false;

            _uow.RecipeDrugRepository.Add(recipeDrug);

            if (_uow.Complete())
            {
                return Ok();
            }
            return BadRequest("Error occured");

        }

        [Authorize(Roles = "Medic")]
        [HttpDelete("recipe/{recipeId}")]
        public ActionResult RemoveRecipeDrug(int recipeId)
        {

            var recipe = _uow.RecipeDrugRepository.Get(s => s.Id == recipeId)
                .FirstOrDefault();
            _uow.RecipeDrugRepository.Remove(recipe);

            if (_uow.Complete())
            {
                return Ok();
            }
            return BadRequest();
        }


        [Authorize(Roles = "Medic, Patient")]
        [HttpDelete("{userId}/recipe/{recipeId}")]
        public async Task<IActionResult> RemoveRecipe(string userId, int recipeId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("No such user");
            }
            var recipe = _uow.RecipeRepository.Get(r => r.Id == recipeId)
                .FirstOrDefault();

            if (await _userManager.IsInRoleAsync(user, "Medic"))
            {
                recipe.IsDeletedForMedic = true;
            }
            if (await _userManager.IsInRoleAsync(user, "Patient"))
            {
                recipe.IsDeletedForPatient = true;
            }

            if (_uow.Complete())
            {
                return Ok();
            }
            return BadRequest("Error occured while deleting recipe");

        }

        [Authorize(Roles = "Medic, Patient, Pharmacist")]
        [HttpGet("recipe/{recipeId}")]
        public async Task<ActionResult> GetAllRecipeDrugs(int recipeId) {

            var recipeDrugs = _uow.RecipeDrugRepository.Get(s => s.RecipeId == recipeId);

            if (recipeDrugs.Count() == 0) {
                return BadRequest("No recipe drugs");
            }

            var model = _mapper.Map<ICollection<RecipeDrugDto>>(recipeDrugs);

            return Ok(model);
            
            
        }

        [Authorize(Roles = "Pharmacist")]
        [HttpPost("{userId}/sell-recipe/{recipeId}")]
        public async Task<IActionResult> SellRecipeDrug(string userId, int recipeDrugId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("No such user");
            }
            var recipe = _uow.RecipeDrugRepository.Get(r => r.Id == recipeDrugId)
                .FirstOrDefault();

            if (await _userManager.IsInRoleAsync(user, "Pharmacist"))
            {
                recipe.IsSold = true;
            }

            if (_uow.Complete())
            {
                return Ok();
            }
            return BadRequest("Error occured while deleting recipe");

        }
    }

}
