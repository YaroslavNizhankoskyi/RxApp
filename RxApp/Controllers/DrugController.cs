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
using RxApp.Helpers;
using RxApp.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RxApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        private readonly UserManager<Customer> _userManager;

        public DrugController(IUnitOfWork uow, IMapper mapper, UserManager<Customer> manager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = manager;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drug>>> GetAll([FromQuery] DrugParams drugParams) {
            
            var drugs = await _uow.DrugRepository.GetDrugsAsync(drugParams);
            if (drugs != null)
            {
                
                Response.AddPaginationHeader(drugs.CurrentPage, drugs.PageSize,
                drugs.TotalCount, drugs.TotalPages);
                var model = _mapper.Map<IEnumerable<DrugDto>>(drugs);
                foreach (var d in model)
                {
                    if (d.PharmGroupId != null)
                    {
                        d.PharmGroup = _uow.PharmGroupRepository
                            .Find(i => i.Id == d.PharmGroupId)
                            .FirstOrDefault()
                            .Name;
                    }
                    else {
                        d.PharmGroupId = 0;
                        d.PharmGroup = "undefined";
                    }

                    var ingredients = _uow.DrugActiveIngredientRepository
                       .Get(u => u.DrugId == d.Id)
                       .Select(u => u.ActiveIngredientId);
                }
                return Ok(model);
            };
            
            return BadRequest("Error occured during getting all drugs");
        }


        [HttpGet("{id}")]
        public ActionResult<DrugInfoDto> Get(int id)
        {
            if (_uow.DrugRepository.Contains(c => c.Id == id))
            {
                Drug drug = _uow.DrugRepository.GetById(id);

                if (drug != null)
                {

                    if (drug.PharmGroupId.HasValue)
                    {
                        drug.PharmGroup = _uow.PharmGroupRepository.GetById(drug.PharmGroupId.Value);
                    }

                    var ingredients = _uow.DrugActiveIngredientRepository
                        .Get(u => u.DrugId == drug.Id)
                        .Select(u => u.ActiveIngredientId);

                    var model = _mapper.Map<DrugInfoDto>(drug);
                    model.Ingredients = ingredients;

                    return Ok(model);
                }
            }

            return NotFound();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(DrugCreateDto model) {
            var ingredientIds = model.Ingredients;

            var createdDrug = _mapper.Map<Drug>(model);

            _uow.DrugRepository.Add(createdDrug);

            if (!_uow.Complete()) return BadRequest();

            if (ingredientIds != null)
            {
                foreach (var ingId in ingredientIds)
                {
                    DrugActiveIngredient activeIng = new DrugActiveIngredient
                    {
                        DrugId = createdDrug.Id,
                        ActiveIngredientId = ingId
                    };

                    _uow.DrugActiveIngredientRepository.Add(activeIng);
                }
            }

            if (_uow.Complete())
            {
                return Ok();
            }

            return BadRequest();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult Put(int id, DrugUpdateDto model)
        {
            if (_uow.DrugRepository.Contains(c => c.Id == id))
            {
                var drug = _uow.DrugRepository.GetById(id);

                var updatedDrug = _mapper.Map(model, drug);

                if (_uow.Complete())
                {
                    return Ok();
                }

                return BadRequest();
            }

            return NoContent();

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (_uow.DrugRepository.Contains(c => c.Id == id)) {
                var drugToRemove = _uow.DrugRepository.GetById(id);
                _uow.DrugRepository.Remove(drugToRemove);

                if (_uow.Complete()) {
                    return Ok();
                }

                return BadRequest();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin, Pharmacist, Medic")]
        [HttpPost("AllergicDrugs/{email}")]
        public async Task<IActionResult> MarkAllergicDrugs(string email, ICollection<int> drugIds) {

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest("No user with such id");
            }

            var allergenes = _uow.AllergyRepository.Get(u => u.CustomerId == user.Id);

            if (allergenes.Count() == 0) {
                return BadRequest("Pacient has no allergies");
            }


            IEnumerable<int> model = new List<int>();


            foreach(var i in drugIds)
            {

                var activeIngredients = _uow.DrugActiveIngredientRepository
                    .Get(s => s.Id == i)
                    .Select(c => c.Id);

                foreach (var ing in activeIngredients) {
                    if (ing == i) {
                        model = model.Append(i);
                        break;
                    }
                }
            }
            return Ok(model);
        }

        [HttpPost("IncompatibleDrugs")]
        public IActionResult MarkIncompatible(IEnumerable<int> drugIds) {

            if (drugIds.Count() == 0) {
                return BadRequest("No drugs to mark");
            }

            List<DrugIngredientsForMark> drugIngredients = new List<DrugIngredientsForMark>();

            var incompatibleIngredients = _uow.IncompatibleIngredientRepository.GetAll();

            foreach (var i in drugIds) {

                var ingredIds = _uow.DrugActiveIngredientRepository
                    .Get(s => s.DrugId == i)
                    .Select(s => s.ActiveIngredientId);

                IEnumerable<IncompatibleIngredient> drugIncompatibleIngreds = new List<IncompatibleIngredient>();

                foreach (var n in ingredIds) {
                    var incompatibleFirst = incompatibleIngredients
                        .Where(s => s.FirstIngredientId == n);

                    var incompatibleSecond = incompatibleIngredients
                        .Where(s => s.SecondIngredientId == n);

                    drugIncompatibleIngreds = drugIncompatibleIngreds.Concat(incompatibleFirst);
                    drugIncompatibleIngreds = drugIncompatibleIngreds.Concat(incompatibleSecond);
                }

                DrugIngredientsForMark drugIngredientsForMark = new DrugIngredientsForMark
                {
                    DrugId = i,
                    IncompatibleIngredientsOfADrug = drugIncompatibleIngreds
                };

                drugIngredients.Add(drugIngredientsForMark);
            }

            for(int i = 0; i < drugIngredients.Count() - 1; i++) {

                if (drugIngredients[i].IsMarked) continue;

                for(int c = i + 1; c < drugIngredients.Count(); c++)
                {
                        
                    var intersectedIngredients = drugIngredients[i].IncompatibleIngredientsOfADrug
                        .Intersect(drugIngredients[c].IncompatibleIngredientsOfADrug);
                    if(intersectedIngredients.Count() > 0) 
                    {
                        drugIngredients[i].IsMarked = true;
                        drugIngredients[c].IsMarked = true;
                    }
                }
            }

            IEnumerable<int> model = new List<int>();

            foreach (var i in drugIngredients)
            {
                if (i.IsMarked)
                {
                    model = model.Append(i.DrugId);
                }
            }

            return Ok(model);
        }


        [Authorize(Roles = "Admin, Medic, Pharmacist")]
        [HttpGet("ingredients/{drugId}")]
        public ActionResult GetDrugIngredients(int drugId) 
        {
            var drugIngredients = _uow.DrugActiveIngredientRepository.Get(s => s.DrugId == drugId);

            IEnumerable<string> ingredients = new List<string>();

            foreach (var i in drugIngredients) 
            {
                var ingredient = _uow.ActiveIngredientRepository.Find(c => c.Id == i.ActiveIngredientId)
                    .FirstOrDefault();

                ingredients = ingredients.Append(ingredient.Name);
            }

            if (ingredients.Count() == 0) 
            {
                return BadRequest("Error during retreiving drug ingredients");
            }
            return Ok(ingredients);
        }

        


    }
}
