using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models
{
    public class PharmGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Drug> Drugs { get; set; }

    }
}
