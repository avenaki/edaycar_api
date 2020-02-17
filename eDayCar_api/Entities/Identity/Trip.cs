using eDayCar.Domain.Entities.Value;
using eDayCar_api.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eDayCar.Domain.Entities.Identity
{
    public class Trip: Entity
    {
        public virtual Place Start { get; set; }
        public virtual Place Finish { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime FinishTime { get; set; }
        public int MaxPassengers { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual List<Passenger> Passengers { get; set; }


    }
}
