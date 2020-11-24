using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models
{
    public class IncompatibleIngredient
    {
        [Key]
        public int Id { get; set; }
        public int FirstIngredientId { get; set; }

        public int SecondIngredientId { get; set; }
    }
}
