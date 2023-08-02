using BackExamen.Data;
using BackExamen.Services;
using Microsoft.AspNetCore.Mvc;
using serverTenis.Data;

namespace BackExamen.Controllers
{

    [ApiController]
    [Route("tenis")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public dynamic Login([FromBody] LoginRequestModel log)
        {
            LoginData.login(log.Password, log.Email);
            if (LoginData.login(log.Password, log.Email) == null)
            {
                return BadRequest("Datos de inicio de sesión inválidos");
            }
            else {
                return LoginData.login(log.Password, log.Email); 
            }
            
          
        }
    }
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}

