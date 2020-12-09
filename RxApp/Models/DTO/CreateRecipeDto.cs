using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class CreateRecipeDto
    {
        public string MedicId { get; set; }

        public string PatientEmail { get; set; }

        public IEnumerable<RecipeDrugDto> RecipeDrugs { get; set; }

    }
}
