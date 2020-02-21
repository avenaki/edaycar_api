using eDayCar.Domain.Entities.Identity;
using System.Collections.Generic;


namespace eDayCar_api.Repositories
{
    public interface IDriverRepository
    {
        void Add(Driver driver);
        void Add(IEnumerable<Driver> drivers);
        List<Driver> Get();
        Driver Get(string username);
        IEnumerable<Driver> Get(IEnumerable<string> usernames);
        void Update(Driver driver);
        void Delete(Driver driver);
        bool Exists(string login, string password);
        bool Exists(string login);
    }
}
