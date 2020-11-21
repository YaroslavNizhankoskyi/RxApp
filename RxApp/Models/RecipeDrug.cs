using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models
{
    public class RecipeDrug
    {
        [Key]
        public int Id { get; set; }

        public Recipe Recipe{ get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        public Drug Drug { get; set; }

        [ForeignKey("Drug")]
        public int DrugId { get; set; }

        public bool IsSold { get; set; }

        public int PerDay { get; set; }

        public string Dose { get; set; }

        public string Comment { get; set; }

        
    }
}
