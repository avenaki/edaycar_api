using eDayCar.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDayCar_api.Services.Abstract
{
    public interface IAccountService
    {

        void RegisterDriver(Driver driver);
        void RegisterPassenger(Passenger passenger);
        bool IsPasswordCorrect(string username, string password);

    }
}
