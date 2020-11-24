using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class RecipeDto
    {
        public int Id { get; set; }

        public string MasterId { get; set; }

        public string SlaveId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DateTime Created { get; set; }
    }
}
