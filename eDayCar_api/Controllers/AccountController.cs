using System.Collections.Generic;
using eDayCar.Domain.Entities.Identity;
using eDayCar_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eDayCar_api.Controllers
{
    [Route("api/account/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IPassengerRepository _passengerRepository;
        
       public AccountController(IDriverRepository driverRepository, IPassengerRepository passengerRepository)
        {
            _driverRepository = driverRepository;
            _passengerRepository = passengerRepository;

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
        public void RegistrDriver([FromBody] Driver value)
        {
            _driverRepository.Add(value);
        }

        [HttpPost]
        public void RegistrPassenger([FromBody] Passenger value)
        {
            _passengerRepository.Add(value);
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
