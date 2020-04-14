using System.Collections.Generic;
using eDayCar.Domain.Entities.Identity;
using eDayCar.Domain.Entities.Value;
using eDayCar_api.Entities.Identity;
using eDayCar_api.Repositories;
using eDayCar_api.Repositories.Abstract;
using eDayCar_api.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDayCar_api.Controllers
{
    [Route("api/request/[action]")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
       
        private readonly IRequestRepository _requestRepository;


        
       public RequestController( IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }
      
        [HttpGet]
        public ActionResult<IEnumerable<Request>> Get()
        {
            return _requestRepository.Get();
        }

   
        [HttpGet("{id}")]
        public ActionResult<Request> Get(string id)
        {
            return _requestRepository.Get(id);
        }

       
        [HttpPost]
        public void CreateRequest([FromBody] Request value)
        {
            _requestRepository.Add(value);
        }

    
        public void Put( [FromBody] Request request)
        {
            _requestRepository.Update(request);
        }

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            return  _requestRepository.Delete(id);
        }
    }
}
