using LBAngularNet.Core.Domain.Entities;
using LBAngularNet.Core.Infraestructure.Security;
using Microsoft.AspNetCore.Mvc;

namespace LBAngularNet.Adapters.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config) 
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Demo demo)
        {
            Token _token = new Token(_config);
             if (demo.Name == "admin" && demo.Password == "admin") 
            {
                var token = _token.GenerateToken(demo.Name);
                return Ok(new { token });
            }
             return Unauthorized(); 
        }
    }
}
