using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class RecipeDrugDtoS
    {
        public int DrugId { get; set; }

        public string Comment { get; set; }

        public int PerDay { get; set; }

        public string Dose { get; set; }

    }
}
