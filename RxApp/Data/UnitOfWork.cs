using RxApp.Data._DrugData;
using RxApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Data
{

    public class UnitOfWork : IUnitOfWork
    {

        private RxAppContext _context;

        private Repository<PharmGroup> pharmGroupRepo;
        private Repository<ActiveIngredient> activeIngredientRepo;
        private DrugRepository drugRepo;
        private Repository<Allergy> allergyRepo;
        private Repository<DrugActiveIngredient> drugActiveIngredientRepo;
        private Repository<Recipe> recipeRepo;
        private Repository<RecipeDrug> recipeDrugRepo;
        private Repository<IncompatibleIngredient> incompatibleIngredintRepo;



        public Repository<PharmGroup> PharmGroupRepository
        {
            get
            {
                return pharmGroupRepo ?? (pharmGroupRepo = new Repository<PharmGroup>(_context));
            }
        }

        public Repository<ActiveIngredient> ActiveIngredientRepository
        {
            get
            {
                return activeIngredientRepo ?? (activeIngredientRepo = new Repository<ActiveIngredient>(_context));
            }
        }

        public Repository<Allergy> AllergyRepository
        {
            get
            {
                return allergyRepo ?? (allergyRepo = new Repository<Allergy>(_context)) ;
            }
        }

        public Repository<DrugActiveIngredient> DrugActiveIngredientRepository
        {
            get
            {
                return drugActiveIngredientRepo ?? (drugActiveIngredientRepo = new Repository<DrugActiveIngredient>(_context));
            }
        }

        public Repository<Recipe> RecipeRepository
        {
            get
            {
                return recipeRepo ?? (recipeRepo = new Repository<Recipe>(_context));
            }
        }

        public Repository<RecipeDrug> RecipeDrugRepository
        {
            get
            {
                return recipeDrugRepo ?? (recipeDrugRepo = new Repository<RecipeDrug>(_context));
            }
        }

        public DrugRepository DrugRepository
        {
            get
            {
                return drugRepo ?? (drugRepo = new DrugRepository(_context));
            }
        }

        public Repository<IncompatibleIngredient> IncompatibleIngredientRepository
        {
            get
            {
                return incompatibleIngredintRepo ?? (incompatibleIngredintRepo  = new Repository<IncompatibleIngredient>(_context));
            }
        }


        public UnitOfWork(RxAppContext context)
        {
            _context = context; 
        }



        public bool Complete()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
