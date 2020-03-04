using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDayCar.Domain.Entities.Identity
{
    public class Driver: User
    {
        public string CarModel { get; set; }
        public string Color { get; set; }
        public int Experience { get; set; }
        public virtual List<Trip> Trips { get; set; }
        public Driver()
        {
            Trips = new List<Trip>();
        }
        public Trip CreateTrip(Trip trip)
        {
            Trips.Add(trip);
            return trip;
        }
     
    }
}
