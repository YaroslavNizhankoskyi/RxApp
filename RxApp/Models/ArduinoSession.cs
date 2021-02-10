using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RxApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RxApp.Models
{
    public class ArduinoSession : ArduinoData
    {


        public static ArduinoData GetData(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext.Session;
            ArduinoSession data = session?.GetJson<ArduinoSession>("ArduinoData")
            ?? new ArduinoSession();
            data.Session = session;
            return data;
        }
        [JsonIgnore]
        public ISession Session { get; set; }


        public void AddData(string doctorId, int drugId)
        {
            base.DoctorId = doctorId;
            base.DrugId = drugId;
            Session.SetJson("ArduinoData", this);
        }
        public void Clear()
        {
            base.DoctorId = null;
            base.DrugId = 0;
            Session.Remove("ArduinoData");
        }
    }
}
