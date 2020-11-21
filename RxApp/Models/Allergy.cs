using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models
{
    public class Allergy
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        public Customer Customer { get; set; }

        [ForeignKey("ActiveIngredient")]
        public int ActiveIngredientId { get; set; }

        public ActiveIngredient ActiveIngredient { get; set; }
    }
}
