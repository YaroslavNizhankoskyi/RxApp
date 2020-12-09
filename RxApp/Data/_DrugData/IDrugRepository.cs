using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RxApp.Helpers;
using RxApp.Models;
using RxApp.Models.DTO;

namespace RxApp.Data._DrugData
{
    interface IDrugRepository : IRepository<Drug>
    {

        Task<PagedList<Drug>> GetDrugsAsync(DrugParams drugParameters);



    }
}
