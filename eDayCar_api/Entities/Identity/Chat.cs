using eDayCar_api.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDayCar_api.Entities.Identity
{
    public class Chat: Entity
    {
        public List <string> Participants { get; set; }
        public List<Message> Messages { get; set; }
     
    }
}
