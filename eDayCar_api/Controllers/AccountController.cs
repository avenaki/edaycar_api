using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using eDayCar.Domain.Entities.Identity;
using eDayCar_api.Repositories;
using eDayCar_api.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace eDayCar_api.Controllers
{
    [Route("api/account/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;
        private readonly IDriverRepository _driverRepository;
        private readonly IPassengerRepository _passengerRepository;

        public AccountController(IAccountService accountService, IDriverRepository driverRepository, IPassengerRepository passengerRepository)
        {
            _accountService = accountService;
            _driverRepository = driverRepository;
            _passengerRepository = passengerRepository;

        }


        [HttpPost]
        public IActionResult RegisterDriver([FromBody] Driver value)
        {
            _accountService.RegisterDriver(value);
            var driverModel = new LoginViewModel(value.Login, value.Password);
            return LoginUser(driverModel);
        }

        [HttpPost]
        public IActionResult RegisterPassenger([FromBody] Passenger value)
        {
            _accountService.RegisterPassenger(value);
            var passengerModel = new LoginViewModel(value.Login, value.Password);
            return LoginUser(passengerModel);
        }

        public class LoginViewModel
        {
            public string login { get; set; }
            public string password { get; set; }

            public LoginViewModel(string login, string password)
            {
                this.login = login;
                this.password = password;
            }
        }


        [HttpPost]
        public IActionResult Login([FromBody]LoginViewModel model)
        {
            return this.LoginUser(model);
        }

        [HttpGet("{login}")]
        public Driver GetDriver(string login)
        {
            return _driverRepository.Get(login); 

        }

        [HttpGet("{login}")]
        public Passenger GetPassenger(string login)
        {
            return _passengerRepository.Get(login);

        }
        [HttpGet]
        public List<Driver> GetDrivers()
        {
            return _driverRepository.Get();

        }

        [HttpGet]
        public List<Passenger> GetPassengers()
        {
            return _passengerRepository.Get();

        }
        private ClaimsIdentity GetIdentity(string username)
        {

            if (_driverRepository.Exists(username))
            {
                var driver = _driverRepository.Get(username);
                return GetIdentityType(driver.Login, "driver");
            }

            if (_passengerRepository.Exists(username))
            {
                var passenger = _passengerRepository.Get(username);
                return GetIdentityType(passenger.Login, "passenger");
            }
            return null;
         
        }

        private ClaimsIdentity GetIdentityType(string login, string role)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)

                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;


        }

        [HttpPut]
        public void UpdateDriver( Driver driver)
        {
            var currentDriver = _driverRepository.Get(driver.Login);
            driver.Id = currentDriver.Id;
            _driverRepository.Update(driver);
        }

        [HttpPut]
        public void UpdatePassenger([FromBody] Passenger passenger)
        {
            var currentPassenger = _passengerRepository.Get(passenger.Login);
            passenger.Id = currentPassenger.Id;
            _passengerRepository.Update(passenger);
        }

        private JsonResult LoginUser(LoginViewModel model)
        {
            if (!_driverRepository.Exists(model.login, model.password))
            {
                if (!_passengerRepository.Exists(model.login, model.password))
                {
                    Response.StatusCode = 400;

                    return null;
                }
            }
            var identity = GetIdentity(model.login);
            if (identity == null)
            {
                Response.StatusCode = 400;
                return null;
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: "edaycar",
                    audience: "edaycar_client",
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromHours(24)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MeGa_S3cR3t_K39!")), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            string userRole = identity.Claims.Last().Value;

            var response = new
            {
                token = encodedJwt,
                login = identity.Name,
                role = userRole
            };

            return new JsonResult(response);
        }
    }

 




}
