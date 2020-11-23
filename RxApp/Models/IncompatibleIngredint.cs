using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models
{
    public class IncompatibleIngredint
    {
        [Key]
        public int Id { get; set; }

        public virtual ActiveIngredient ActiveIngredientFirst{ get; set; }

        public virtual ActiveIngredient ActiveIngredientSecond { get; set; }


        public int ActiveIngredientFirstId { get; set; }

        public int ActiveIngredintSecondId { get; set; }
    }
}
