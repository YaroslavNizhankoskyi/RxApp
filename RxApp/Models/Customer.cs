using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models
{
    public class Customer : IdentityUser
    {
        public Customer()
        {
            Allergies = new HashSet<Allergy>();
        }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public int Age { get; set; }

        public virtual ICollection<Allergy> Allergies { get; set; }

        public string WorkName { get; set; }

        public bool AllowedAddingRecipes { get; set; }


    }
}
