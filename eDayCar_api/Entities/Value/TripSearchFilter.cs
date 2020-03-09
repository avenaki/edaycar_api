using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace eDayCar.Domain.Entities.Value
{
    public class TripSearchFilter
    {
        public int CanWalkDistance { get; set; }
        public double StartX { get; set; }
        public double StartY { get; set; }
        public double FinishX { get; set; }
        public double FinishY { get; set; }

        public string StartTime { get; set; }

        public string FinishTime { get; set; }
      
    }
}