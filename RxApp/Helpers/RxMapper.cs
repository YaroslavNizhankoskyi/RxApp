using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RxApp.Models;
using RxApp.Models.DTO;

namespace RxApp.Helpers
{
    public class RxMapper :Profile
    {
        public RxMapper()
        {
            CreateMap<DrugUpdateDto, Drug>();

            CreateMap<Drug, DrugInfoDto>()
                .ForMember(dest => dest.PharmGroupName, src => src.MapFrom(s => s.PharmGroup.Name))
                .ForMember(dest => dest.DrugActiveIngredientNames,
                    src => src.MapFrom(
                        s => s.DrugActiveIngredients.Select(
                            p => p.ActiveIngredient.Name)));

            CreateMap<DrugCreateDto, Drug>();

            CreateMap<ChangeProfileDto, Customer>();

            CreateMap<ActiveIngredient, IngredientDto>();

            CreateMap<PharmGroup, PharmGroupDto>();

            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.Created, src => src.MapFrom(s => s.Time));

            CreateMap<RecipeDrugDto, RecipeDrug>();

                
        }
    }
}
