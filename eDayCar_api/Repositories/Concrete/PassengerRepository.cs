using eDayCar.Domain.Entities.Identity;
using eDayCar_api.Repositories;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;


namespace eDayCar.Domain.Repositories.Concrete
{
    public class PassengerRepository: IPassengerRepository
    {
        private IMongoCollection<Passenger> Collection { get; }

        public PassengerRepository(MongoContext context)
        {
            Collection = context.GetCollection<Passenger>("Passengers");
        }
       
        public void Add(Passenger passenger)
        {
            Collection.InsertOne(passenger);
        }

        public void Add(IEnumerable<Passenger> passengers)
        {
            Collection.InsertMany(passengers);
        }

        List<Passenger> Get()
        {
            return Collection.Find(_ => true).ToList();
        }

        public Passenger Get(string username)
        {
            return Collection.Find(i => i.Username == username).FirstOrDefault();
        }

        public IEnumerable<Passenger> Get(IEnumerable<string> usernames)
        {
            var filter = Builders<Passenger>.Filter.In(i => i.Username, usernames);
            return Collection.Find(filter).ToList();
        }

        public void Update(Passenger passenger)
        {
            var filter = Builders<Passenger>.Filter.Where(i => i.Username == passenger.Username);
            Collection.ReplaceOne(filter, passenger);
        }

        public void Delete(Passenger passenger)
        {

            var filter = Builders<Passenger>.Filter.Where(i => i.Username == passenger.Username);
            Collection.DeleteOne(filter);
        }

        List<Passenger> IPassengerRepository.Get()
        {
            return Collection.Find(_ => true).ToList();
        }
    }
}
