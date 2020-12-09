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

        public string SlaveEmail { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
