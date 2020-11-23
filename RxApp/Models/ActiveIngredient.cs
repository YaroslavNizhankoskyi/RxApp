using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models
{
    public class ActiveIngredient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Allergy> Allergies { get; set; }
        public virtual ICollection<IncompatibleIngredint> IncompatibleIngredintFirst{ get; set; }
        public virtual ICollection<IncompatibleIngredint> IncompatibleIngredintSecond{ get; set; }



    }
}
