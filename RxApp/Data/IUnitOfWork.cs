using RxApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Data
{
    public interface IUnitOfWork : IDisposable
    {

        Repository<PharmGroup> PharmGroupRepository{ get; }
        Repository<Drug> DrugRepository { get; }
        Repository<ActiveIngredient> ActiveIngredientRepository { get; }
        Repository<Allergy> AllergyRepository { get; }
        Repository<DrugActiveIngredient> DrugActiveIngredientRepository { get; }
        Repository<Recipe> RecipeRepository { get; }
        Repository<RecipeDrug> RecipeDrugRepository { get; }

        bool Complete();

    }
}
