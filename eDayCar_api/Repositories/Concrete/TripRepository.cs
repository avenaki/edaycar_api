using eDayCar.Domain.Entities.Identity;
using eDayCar.Domain.Entities.Value;
using eDayCar_api.Repositories;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;


namespace eDayCar.Domain.Repositories.Concrete
{
    public class TripRepository: ITripRepository
    {
        private IMongoCollection<Trip> Collection { get; }

        public TripRepository(MongoContext context)
        {
            Collection = context.GetCollection<Trip>("Trips");
        }

        
        public void Add(Trip trip)
        {
            Collection.InsertOne(trip);
        }

        public void Add(IEnumerable<Trip> trips)
        {
            Collection.InsertMany(trips);
        }

        List<Trip> Get()
        {
            return Collection.Find(_ => true).ToList();
        }

        public Trip Get(string id)
        {
            return Collection.Find(i => i.Id == id).FirstOrDefault();
        }

        public IEnumerable<Trip> Get(IEnumerable<string> ids)
        {
            var filter = Builders<Trip>.Filter.In(i => i.Id, ids);
            return Collection.Find(filter).ToList();
        }

        public void Update(Trip trip)
        {
            var filter = Builders<Trip>.Filter.Where(i => i.Id == trip.Id);
            Collection.ReplaceOne(filter, trip);
        }

        public void Delete(string id)
        {

            var filter = Builders<Trip>.Filter.Where(i => i.Id == id);
            Collection.DeleteOne(filter);
        }


        public IEnumerable<Trip> Get(TripSearchFilter filter)
        {
            var filtr = Builders<Trip>.Filter.Where(t =>
           filter.FinishTime.AddMinutes(-30).TimeOfDay <= t.FinishTime.TimeOfDay
           && filter.FinishTime.AddMinutes(30).TimeOfDay >= t.FinishTime.TimeOfDay
           && (new GeoCoordinate(filter.StartX, filter.StartY).GetDistanceTo
           (new GeoCoordinate(t.StartX, t.StartY)) <= filter.CanWalkDistance)
           && (new GeoCoordinate(filter.FinishX, filter.FinishY).GetDistanceTo
           (new GeoCoordinate(t.FinishX, t.FinishY)) <= filter.CanWalkDistance)
           && (t.MaxPassengers > 0));
            return Collection.Find(filtr).ToList();
        }

        List<Trip> ITripRepository.Get()
        {
            return Collection.Find(_ => true).ToList();
        }
    }
}
