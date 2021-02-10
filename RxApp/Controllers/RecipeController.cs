using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RxApp.Data;
using RxApp.Helpers;
using RxApp.Models;
using RxApp.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RxApp.Controllers
{
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
        [HttpGet("{email}")]
        public async Task<IActionResult> GetAll(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
            IEnumerable<RecipeDto> model = new List<RecipeDto>();


            if (user == null)
            {
                return BadRequest("No such user");
            }

            IEnumerable<Recipe> recipes = new List<Recipe>();

            if (await _userManager.IsInRoleAsync(user, "Patient"))
            {
                recipes = _uow.RecipeRepository
                    .Get(s => s.PatientId == user.Id);

                if (recipes.Count() == 0)
                {
                    return BadRequest("No recipes");
                }

                foreach (var i in recipes)
                {
                    var slaveEmail = (await _userManager.FindByIdAsync(i.PatientId)).Email;
                    var dto = new RecipeDto
                    {
                        Id = i.Id,
                        MasterId = user.Id,
                        SlaveEmail = slaveEmail,
                        DateCreated = i.DateCreated
                    };

                    model = model.Append(dto);

                }
            }
            else if (await _userManager.IsInRoleAsync(user, "Medic"))
            {
                recipes = _uow.RecipeRepository
                    .Get(s => s.MedicId == user.Id);
                if (recipes.Count() == 0)
                {
                    return BadRequest("No recipes");
                }

                foreach (var i in recipes)
                {
                    var slaveEmail = (await _userManager.FindByIdAsync(i.PatientId)).Email;
                    var dto = new RecipeDto
                    {
                        Id = i.Id,
                        MasterId = user.Id,
                        SlaveEmail = slaveEmail,
                        DateCreated = i.DateCreated
                    };
                    model = model.Append(dto);
                }

            }
            else {
                return BadRequest("Invalid user data");
            }
            return Ok(model);

        }


        [Authorize(Roles = "Medic")]
        [HttpPost]
        public async Task<IActionResult> CreateRecipe(CreateRecipeDto model)
        {

            var medic = await _userManager.FindByIdAsync(model.MedicId);

            if (medic == null)
            {
                return BadRequest("No such user");
            }

            var user = await _userManager.FindByEmailAsync(model.PatientEmail);

            var recipe = new Recipe();

            if (await _userManager.IsInRoleAsync(medic, "Medic"))
            {


                recipe = new Recipe
                {
                    MedicId = model.MedicId,
                    PatientId = user.Id,
                    IsDeletedForMedic = false,
                    IsDeletedForPatient = false,
                    DateCreated = DateTime.Now
                };

                _uow.RecipeRepository.Add(recipe);
                _uow.Complete();
            }


            foreach (var r in model.RecipeDrugs)
            {
                var recipeDrug = _mapper.Map<RecipeDrug>(r);

                recipeDrug.IsSold = false;

                recipeDrug.RecipeId = recipe.Id;

                _uow.RecipeDrugRepository.Add(recipeDrug);

            }

            _uow.Complete();
            return Ok();

        }



        [Authorize(Roles = "Medic, Patient")]
        [HttpDelete("{userId}/recipe/{recipeId}")]
        public async Task<IActionResult> HideRecipe(string userId, int recipeId)
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
        [HttpGet("drugs/{recipeId}")]
        public async Task<ActionResult> GetAllRecipeDrugs(int recipeId) {

            var recipeDrugs = _uow.RecipeDrugRepository.Get(s => s.RecipeId == recipeId);

            ArrayList model = new ArrayList();
            foreach (var drug in recipeDrugs)
            {
                var drugFromDb = _uow.DrugRepository.Find(d => d.Id == drug.DrugId)
                    .FirstOrDefault();

                var recipe = new RecipeDrugDto
                {
                    DrugId = drug.DrugId,
                    NameEn = drugFromDb.NameEng,
                    NameUa = drugFromDb.NameRus,
                    IsSold = drug.IsSold,
                    PerDay = drug.PerDay,
                    Comment = drug.Comment,
                    Dose = drug.Dose
                };
                model.Add(recipe);

            }
            if (recipeDrugs.Count() == 0) {
                return BadRequest("No recipe drugs");
            }

            return Ok(model.ToArray());
            
            
        }

        [Authorize(Roles = "Pharmacist")]
        [HttpPost("{userId}/sell-recipe/{recipeDrugId}")]
        public async Task<IActionResult> SellRecipeDrug(string userId, int recipeDrugId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("No such user");
            }
            var recipe = _uow.RecipeDrugRepository.Get(r => r.Id == recipeDrugId)
                .FirstOrDefault();



            ArduinoData data = new ArduinoData
            {
                DoctorId = userId,
                DrugId = recipe.DrugId
            };

            SaveData(data);
            return Ok();


        }

        [HttpGet("Arduino")]
        public IActionResult GetArduinoData() {
            var data = GetCart();

            if (data.DoctorId != null) 
            {
                return Ok(data);
            }

            return BadRequest();

        }

        private ArduinoData GetCart()
        {
            ArduinoData data = HttpContext.Session.GetJson<ArduinoData>("ArduinoData") ?? new ArduinoData();
            return data;
        }

        private void SaveData(ArduinoData data)
        {
            HttpContext.Session.SetJson("ArduinoData", data);
        }

        [HttpGet("Test")]
        public IActionResult Test() 
        {

            var hello = "Henlo";
            return Ok(hello);
        }
    }

}
