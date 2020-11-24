using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class DrugIngredientsForMark
    {
        public int DrugId { get; set; }

        public IEnumerable<IncompatibleIngredient> IncompatibleIngredientsOfADrug { get; set; }

        public bool IsMarked { get; set; }
    }
}
