using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RxApp.Models;
using RxApp.Models.DTO;

namespace RxApp.Data._DrugData
{
    interface IDrugRepository : IRepository<Drug>
    {

        IEnumerable<Drug> GetAllFiltered(DrugParams drugParameters);



    }
}
