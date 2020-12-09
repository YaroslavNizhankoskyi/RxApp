using Microsoft.EntityFrameworkCore;
using RxApp.Helpers;
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

        public async Task<PagedList<Drug>> GetDrugsAsync(DrugParams drugParameters)
        {
            IEnumerable<Drug> drugs = dbSet.AsEnumerable();

            if (drugParameters.PharmGroupId > 0)
            {
                drugs = drugs.Where(d => d.PharmGroupId == drugParameters.PharmGroupId);
            }

            if (!string.IsNullOrEmpty(drugParameters.DrugName))
            {
                drugs = drugParameters.Eng switch
                {
                    true => drugs.Where(b => b.NameEng.ToLower().Contains(drugParameters.DrugName.ToLower())),
                    false => drugs.Where(b => b.NameRus.ToLower().Contains(drugParameters.DrugName.ToLower()))
                };
            }

            drugs = drugParameters.Eng switch
            {
                true => drugs = drugParameters.AlphabeticalOrderAsc switch
                {
                    true => drugs.OrderBy(u => u.NameEng),
                    false => drugs.OrderByDescending(u => u.NameEng)
                },
                false =>
                drugs = drugParameters.AlphabeticalOrderAsc switch
                {
                    true => drugs.OrderBy(u => u.NameRus),
                    false => drugs.OrderByDescending(u => u.NameRus)
                }
            };



            return await PagedList<Drug>.CreateAsync(drugs,
                    drugParameters.PageNumber, drugParameters.PageSize);
        }
    }
}
