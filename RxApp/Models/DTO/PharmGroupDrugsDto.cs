using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class PharmGroupDrugsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
