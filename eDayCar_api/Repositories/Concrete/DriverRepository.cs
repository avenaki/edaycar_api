using eDayCar.Domain.Entities.Identity;
using eDayCar_api.Repositories;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;


namespace eDayCar.Domain.Repositories.Concrete
{
    public class DriverRepository: IDriverRepository
    {
        private IMongoCollection<Driver> Collection { get; }

        public DriverRepository(MongoContext context)
        {
            Collection = context.GetCollection<Driver>("Drivers");
        }

        public void Add(Driver driver)
        {
            Collection.InsertOne(driver);
        }

        public void Add(IEnumerable<Driver> drivers)
        {
            Collection.InsertMany(drivers);
        }

        List<Driver> Get()
        {
            return Collection.Find(_ => true).ToList();
        }

        public Driver Get(string login)
        {
            return Collection.Find(i => i.Login == login).FirstOrDefault();
        }

        public IEnumerable<Driver> Get(IEnumerable<string>logins)
        {
            var filter = Builders<Driver>.Filter.In(i => i.Login, logins);
            return Collection.Find(filter).ToList();
        }

        public void Update(Driver driver)
        {
            var filter = Builders<Driver>.Filter.Where(i => i.Login == driver.Login);
            Collection.ReplaceOne(filter, driver);
        }

        public void Delete(Driver driver)
        {

            var filter = Builders<Driver>.Filter.Where(i => i.Login == driver.Login);
            Collection.DeleteOne(filter);
        }

        List<Driver> IDriverRepository.Get()
        {
            return Collection.Find(_ => true).ToList();
        }

        public bool Exists(string login, string password)
        {
            return Collection.Find(i => i.Login == login && i.Password == password).FirstOrDefault() != null;
        }

        public bool Exists(string login)
        {
            return Collection.Find(i => i.Login == login).FirstOrDefault() != null;
        }
    }
}
