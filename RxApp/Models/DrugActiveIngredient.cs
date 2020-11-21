using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models
{
    public class DrugActiveIngredient
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ActiveIngredient")]
        public int ActiveIngredientId { get; set; }

        public ActiveIngredient ActiveIngredient { get; set; }

        [ForeignKey("Drug")]
        public int DrugId { get; set; }

        public Drug Drug { get; set; }
    }
}
