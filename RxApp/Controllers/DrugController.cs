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

                    var ingredients = _uow.DrugActiveIngredientRepository.Get(u => u.DrugId == drug.Id);

                    var model = _mapper.Map<DrugInfoDto>(drug);

                    return Ok(model);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult Create(DrugCreateDto model) {
            var ingredientIds = model.Ingredients;

            var createdDrug = _mapper.Map<Drug>(model);

            _uow.DrugRepository.Add(createdDrug);

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

        [HttpGet("Mark/{id}")]
        public async Task<IActionResult> MarkAllergicDrugs(string id, ICollection<int> drugIds) {

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest("No user with such id");
            }

            var allergenes = _uow.AllergyRepository.Get(u => u.CustomerId == id);

            if (allergenes.Count() == 0) {
                return BadRequest("Pacient has no allergies");
            }


            var model = new AllergicDrugsDto
            {
                MarkedDrugs = new List<MarkedDrug>()
            };

            foreach(var i in drugIds)
            {
                var markedDrug = new MarkedDrug
                {
                    DrugId = i,
                    HasAllergene = false
                };


                var activeIngredients = _uow.DrugActiveIngredientRepository
                    .Get(s => s.Id == i)
                    .Select(c => c.Id);

                foreach (var ing in activeIngredients) {
                    if (ing == i) {
                        markedDrug.HasAllergene = true;      
                    }
                }

                model.MarkedDrugs.Add(markedDrug);

            }

            return Ok(model);
        }


    }
}
