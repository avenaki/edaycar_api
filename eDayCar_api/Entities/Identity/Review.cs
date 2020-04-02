using eDayCar_api.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDayCar_api.Entities.Identity
{
    public class Review: Entity
    {
        public string AuthorLogin { get; set;}
        public string UserLogin   { get; set;}
        public string ReviewText  { get; set;}
        public DateTime SendTime  { get; set;}
    }
}
