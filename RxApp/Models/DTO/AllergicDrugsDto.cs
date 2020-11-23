using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class AllergicDrugsDto
    {
        public ICollection<MarkedDrug> MarkedDrugs { get; set; }
    }
}
