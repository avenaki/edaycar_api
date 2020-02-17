using eDayCar.Domain.Entities.Identity;
using System.Collections.Generic;


namespace eDayCar_api.Repositories
{
   public interface IPassengerRepository
    {
        void Add(Passenger passenger);
        void Add(IEnumerable<Passenger> passengers);
        List<Passenger> Get();
        Passenger Get(string username);
        IEnumerable<Passenger> Get(IEnumerable<string> usernames);
        void Update(Passenger passenger);
        void Delete(Passenger passenger);
    }
}
