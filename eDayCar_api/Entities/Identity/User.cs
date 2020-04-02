using eDayCar_api.Entities.Base;
using eDayCar_api.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eDayCar.Domain.Entities.Identity
{
    public abstract class User: Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string MobileNumber { get; set; }
        public string AccountPhoto { get; set; }
        public DateTime Birthdate { get; set; }
        public List<string> ReviewsId { get; set; }
        public List<string> RequestsId { get; set; }
    }    
}
