using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class MarkedDrug
    {
        public int DrugId { get; set; }

        public string DrugName { get; set; }

        public bool HasAllergene { get; set; }
    }
}
