using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class UserInfoDto
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public bool AlloedAddRecipes { get; set; }
    }
}
