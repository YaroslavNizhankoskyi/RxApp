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

        public virtual ICollection<Allergy> Allergies { get; set; }

        public string WorkName { get; set; }


    }
}
