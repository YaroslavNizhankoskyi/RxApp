using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class RecipeDrugDto
    {
        public int DrugId { get; set; }

        public string Dose { get; set; }

        public string Comment { get; set; }

        public int PerDay { get; set; }

        public string NameUa { get; set; }

        public string NameEn { get; set; }

        public bool IsSold{ get; set; }
    }
}
