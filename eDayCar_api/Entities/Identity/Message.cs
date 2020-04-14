using eDayCar_api.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDayCar_api.Entities.Identity
{
    public class Message: Entity
    {
      public string Receiver { get; set; }
      public string Sender { get; set; }
      public string Text { get; set; }
      public string Type { get; set; }
      public DateTime SendTime { get; set; }
}
}
