using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string Id { get; set; }

    }
}
