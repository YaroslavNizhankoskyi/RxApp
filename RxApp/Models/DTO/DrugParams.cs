using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class DrugParams
    {
        public int PharmGroupId{ get; set; }

        public string DrugName { get; set; }

        public bool AlphabeticalOrderAsc { get; set; } = true;

        public bool Eng { get; set; } = false;
    }
}
