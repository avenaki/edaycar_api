using eDayCar.Domain.Entities.Identity;
using eDayCar_api.Repositories;
using eDayCar_api.Services.Abstract;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

       


        public void RegisterDriver( Driver driver)
        {
            var existingDriver = _driverRepository.Get(driver.Login);
            if (existingDriver == null)
 
            {   var existingPassenger = _passengerRepository.Get(driver.Login);
                if (existingPassenger == null)
                { _driverRepository.Add(driver); }
            }
            else
            {
                throw new Exception("Username уже используется!");
            }

        }

        public void RegisterPassenger(Passenger passenger)
        {
            var existingDriver = _driverRepository.Get(passenger.Login);
           if (existingDriver == null)

            {
                var existingPassenger = _passengerRepository.Get(passenger.Login);

                if (existingPassenger == null)
                    _passengerRepository.Add(passenger);
            }
            else
            {
                throw new Exception("Username уже используется!");
            }
        }
    }
}
