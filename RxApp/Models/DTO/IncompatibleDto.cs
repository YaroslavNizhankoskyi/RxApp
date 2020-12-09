using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class IncompatibleDto
    {
        public string NameFirst { get; set; }

        public string NameSecond { get; set; }

        public int IncompatibleId { get; set; }

        public int IdFirst { get; set; }

        public int IdSecond { get; set; }
    }
}
