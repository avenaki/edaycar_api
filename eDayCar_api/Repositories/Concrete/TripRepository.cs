using eDayCar.Domain.Entities.Identity;
using eDayCar.Domain.Entities.Value;
using eDayCar_api.Repositories;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using GeoCoordinatePortable;
using System;

namespace eDayCar.Domain.Repositories.Concrete
{
    public class TripRepository : ITripRepository
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

        public string Delete(string id)
        {
            try
            {
                var filter = Builders<Trip>.Filter.Where(i => i.Id == id);
                Collection.DeleteOne(filter);
                return "success";
            }
            catch
            {
                return "fail";
            }
        }


        public IEnumerable<Trip> Get(TripSearchFilter filter)
        {
            var startTime = TimeSpan.Parse(filter.StartTime);
            var finishTime = TimeSpan.Parse(filter.FinishTime);
            var allTrips = Get();
            var fitByTime = allTrips.Where(t =>
           finishTime.Add(TimeSpan.FromMinutes(-30)) <= t.FinishTime.ToUniversalTime().TimeOfDay
           && finishTime.Add(TimeSpan.FromMinutes(30)) >= t.FinishTime.ToUniversalTime().TimeOfDay).ToList();
            var fitByFinish = fitByTime.Where(t =>
            new GeoCoordinate(filter.StartX, filter.StartY).GetDistanceTo(new GeoCoordinate(t.StartX, t.StartY)) <= filter.CanWalkDistance
           && new GeoCoordinate(filter.FinishX, filter.FinishY).GetDistanceTo
           (new GeoCoordinate(t.FinishX, t.FinishY)) <= filter.CanWalkDistance).ToList();
            var fit = fitByFinish.Where(t => t.MaxPassengers > 0).ToList();
            return fit.ToList();
        }

        List<Trip> ITripRepository.Get()
        {
            return Collection.Find(_ => true).ToList();
        }
    }
}
