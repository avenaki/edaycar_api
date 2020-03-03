using eDayCar_api.Entities.Base;
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

    }
}
