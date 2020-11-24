using RxApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RxApp.Data;
using RxApp.Data._DrugData;

namespace RxApp.Data
{
    public interface IUnitOfWork : IDisposable
    {

        Repository<PharmGroup> PharmGroupRepository{ get; }
        DrugRepository DrugRepository { get; }
        Repository<ActiveIngredient> ActiveIngredientRepository { get; }
        Repository<Allergy> AllergyRepository { get; }
        Repository<DrugActiveIngredient> DrugActiveIngredientRepository { get; }
        Repository<Recipe> RecipeRepository { get; }
        Repository<RecipeDrug> RecipeDrugRepository { get; }
        Repository<IncompatibleIngredient> IncompatibleIngredientRepository { get; }

        bool Complete();

    }
}
