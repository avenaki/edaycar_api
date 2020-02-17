using eDayCar.Domain.Entities.Identity;
using eDayCar_api.Repositories;
using eDayCar_api.Services.Abstract;
using System;

namespace eDayCar_api.Services.Concrete
{

    public class AccountService : IAccountService
    {
        private IDriverRepository _driverRepository;
        private IPassengerRepository _passengerRepository;
        public AccountService(IDriverRepository driverRepository, IPassengerRepository passengerRepository)
        {
            _driverRepository = driverRepository;
            _passengerRepository = passengerRepository;
        }
        public bool IsPasswordCorrect(string email, string password)
        {
            throw new System.NotImplementedException();
        }


        public void RegisterDriver( Driver driver)
        {
            var existingDriver = _driverRepository.Get(driver.Username);
            if (existingDriver == null)
                _driverRepository.Add(driver);
            else
            {
                throw new Exception("Username уже используется!");
            }

        }

        public void RegisterPassenger(Passenger passenger)
        {
            throw new NotImplementedException();
        }
    }
}
