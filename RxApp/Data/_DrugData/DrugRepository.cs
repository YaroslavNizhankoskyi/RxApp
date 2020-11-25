using Microsoft.EntityFrameworkCore;
using RxApp.Models;
using RxApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Data._DrugData
{
    public class DrugRepository : Repository<Drug>, IDrugRepository
    {
        public DrugRepository(DbContext context) :base(context)
        {
                
        }

        public IEnumerable<Drug> GetAllFiltered(DrugParams drugParameters)
        {
            IEnumerable<Drug> drugs = dbSet;

            if (drugParameters.Eng && !string.IsNullOrEmpty(drugParameters.DrugName))
            {
                if (drugs.Count() > 0)
                {
                    drugs = drugs.Where(b => b.NameEng.ToLower().Contains(drugParameters.DrugName.ToLower()));
                }

                if (drugParameters.AlphabeticalOrderAsc && drugs.Count() > 0)
                {
                    drugs = drugs.OrderBy(s => s.NameEng);
                }
                else if (drugs.Count() > 0)
                {
                    drugs = drugs.OrderByDescending(s => s.NameEng);
                }
            }
            else if(!string.IsNullOrEmpty(drugParameters.DrugName))
            {

                if (drugs.Count() > 0)
                {
                    drugs = drugs.Where(b => b.NameRus.ToLower().Contains(drugParameters.DrugName.ToLower()));
                }


                if (drugParameters.AlphabeticalOrderAsc && drugs.Count() > 0)
                {
                    drugs = drugs.OrderBy(s => s.NameRus);

                }
                else if (drugs.Count() > 0)
                {
                    drugs = drugs.OrderByDescending(s => s.NameRus);
                }

            }

            if (drugParameters.PharmGroupId != 0 && drugs.Count() > 0)
            {
                drugs = drugs.Where(s => s.PharmGroupId == drugParameters.PharmGroupId);
            }

            return drugs;
        }
    }
}
