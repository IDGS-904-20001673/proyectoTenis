using BackExamen.Controllers;
using Microsoft.AspNetCore.Mvc;
using serverTenis.Data;
using tenis.Data;
using tenis.Models;

namespace tenis.Controllers
{

    [ApiController]
    [Route("tenis")]
    public class ProvedoresController
    {
        [HttpPost]
        [Route("RegistroProvedor")]
        public dynamic Login([FromBody] Provedor pro)
        {

                return ProveedoresData.RegistrarProvedor(pro);
            }


        }
    }

