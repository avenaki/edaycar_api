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

        public Driver Get(string username)
        {
            return Collection.Find(i => i.Username == username).FirstOrDefault();
        }

        public IEnumerable<Driver> Get(IEnumerable<string> usernames)
        {
            var filter = Builders<Driver>.Filter.In(i => i.Username, usernames);
            return Collection.Find(filter).ToList();
        }

        public void Update(Driver driver)
        {
            var filter = Builders<Driver>.Filter.Where(i => i.Username == driver.Username);
            Collection.ReplaceOne(filter, driver);
        }

        public void Delete(Driver driver)
        {

            var filter = Builders<Driver>.Filter.Where(i => i.Username == driver.Username);
            Collection.DeleteOne(filter);
        }

        List<Driver> IDriverRepository.Get()
        {
            return Collection.Find(_ => true).ToList();
        }
    }
}
