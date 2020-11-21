using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Models.DTO
{
    public class DrugCreateDto
    {
        public int PharmGroupId { get; set; }

        public ICollection<int> Ingredients { get; set; }

        public string NameRus { get; set; }

        public string NameEng { get; set; }

        public string Action { get; set; }

        public string Dosing { get; set; }

        public string Overdose { get; set; }

        public string StorageCondition { get; set; }

        public string Nozology { get; set; }

        public string Packaging { get; set; }

        public string Indications { get; set; }

        public string BestBefore { get; set; }

        public string SpecialCases { get; set; }

        public string SideEffects { get; set; }

        public string DuringPregnancy { get; set; }

        public string Pharmacodynamics { get; set; }

        public string Pharmacokinetics { get; set; }
    }
}
