using eDayCar_api.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDayCar_api.Entities.Identity
{
    
    public class Request: Entity
    {
        public string DriverLogin { get; set; }
        public string PassengerLogin { get; set; }
        public string TripId { get; set; }
        public RequestStatus Status { get; set; }
    }
}
