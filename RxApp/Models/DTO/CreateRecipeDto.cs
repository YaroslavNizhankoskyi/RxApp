using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class CreateRecipeDto
    {
        public string MedicId { get; set; }

        public string PatientId { get; set; }

        public DateTime Start { get; set; }

        public DateTime Time { get; set; }

        public DateTime End { get; set; }

        public virtual ICollection<int> RecipeDrugsIds { get; set; }

    }
}
