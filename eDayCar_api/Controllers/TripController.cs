using System.Collections.Generic;
using eDayCar.Domain.Entities.Identity;
using eDayCar.Domain.Entities.Value;
using eDayCar_api.Repositories;
using eDayCar_api.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDayCar_api.Controllers
{
    [Route("api/trip/[action]")]
    [ApiController]
    [Authorize]
    public class TripController : ControllerBase
    {
       
        private readonly ITripRepository _tripRepository;
        private readonly IPassengerRepository _passengerRepository;

        
       public TripController(ITripRepository tripRepository, IPassengerRepository passengerRepository)
        {
            _tripRepository = tripRepository;
            _passengerRepository = passengerRepository;

        }
        public class TakeTripViewModel
        {
            public Trip trip { get; set; }
            public string login { get; set; }

            public TakeTripViewModel(Trip trip, string login)
            {
                this.trip = trip;
                this.login = login;
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<Trip>> Get()
        {
            return _tripRepository.Get();
        }

   
        [HttpGet("{id}")]
        public ActionResult<Trip> Get(string id)
        {
            return _tripRepository.Get(id);
        }

        [HttpPost]
        public IEnumerable<Trip> FilterTrips([FromBody] TripSearchFilter filter)
        {
            return _tripRepository.Get(filter);
        }
        [HttpPost]
        public void CreateTrip([FromBody] Trip value)
        {
            _tripRepository.Add(value);
        }

     [HttpPost]
     public void TakeTrip([FromBody] TakeTripViewModel info)
        {
            info.trip.MaxPassengers = info.trip.MaxPassengers - 1;
            if(info.trip.PassengersLogins == null)
            {
                info.trip.PassengersLogins = new List<string>();
            }
            info.trip.PassengersLogins.Add(info.login);
            _tripRepository.Update(info.trip);
         
        }
    
        public void Put( [FromBody] Trip trip)
        {
            _tripRepository.Update(trip);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete( string id)
        {
            return new JsonResult( _tripRepository.Delete(id));
        }
    }
}
