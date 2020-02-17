﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace eDayCar.Domain.Entities.Value
{
    public class TripSearchFilter
    {
        public int CanWalkDistance { get; set; }
        public Place Start { get; set; }
        public Place Finish { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime FinishTime { get; set; }
      
    }
}