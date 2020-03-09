using System.Collections.Generic;
using eDayCar.Domain.Entities.Identity;
using eDayCar.Domain.Entities.Value;
using eDayCar_api.Repositories;
using eDayCar_api.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace eDayCar_api.Controllers
{
    [Route("api/trip/[action]")]
    [ApiController]
    public class TripController : ControllerBase
    {
       
        private readonly ITripRepository _tripRepository;
        
       public TripController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository; 

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

     

    
        public void Put( [FromBody] Trip trip)
        {
            _tripRepository.Update(trip);
        }

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            return _tripRepository.Delete(id);
        }
    }
}
