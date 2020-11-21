using Microsoft.EntityFrameworkCore;
using RxApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Data.DrugData
{
    public class DrugRepository : Repository<Drug>, IDrugRepository
    {
        public DrugRepository(DbContext context) :base(context)
        {
                
        }
    }
}
