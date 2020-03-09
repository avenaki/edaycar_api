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
        public double StartX { get; set; }
        public double StartY { get; set; }
        public double FinishX { get; set; }
        public double FinishY { get; set; }
        public string StartPlace { get; set; }
        public string FinishPlace { get; set; }
        public int MaxPassengers { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime FinishTime { get; set; }
        public virtual string DriverLogin { get; set; }
        public virtual List<String> PassengersLogins { get; set; }


    }
}
