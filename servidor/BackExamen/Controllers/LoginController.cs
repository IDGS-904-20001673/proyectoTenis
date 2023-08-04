﻿using BackExamen.Data;
using BackExamen.Services;
using Microsoft.AspNetCore.Mvc;
using serverTenis.Data;
using serverTenis.Models;
using tenis.Data;

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
            if (LoginData.login(log.Password, log.Email) == false)
            {
                return BadRequest("Datos de inicio de sesión inválidos");
            }
            else {
                return LoginData.login(log.Password, log.Email); 
            }
            
          
        }



        [HttpPost]
        [Route("Registrase")]
        public dynamic Registrarse([FromBody] Usuario usuario )
        {          
            return RegistrarseData.Registrar(usuario);

        }


    }
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }



}
