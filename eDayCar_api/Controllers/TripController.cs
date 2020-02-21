using System.Collections.Generic;
using eDayCar.Domain.Entities.Identity;
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
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void CreateTrip([FromBody] Trip value)
        {
            _tripRepository.Add(value);
        }

        [HttpPost]
        public void RegisterPassenger([FromBody] Passenger value)
        {
           
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
