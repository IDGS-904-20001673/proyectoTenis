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
        public dynamic RegistarProveedor([FromBody] Provedor pro)
        {

                return ProveedoresData.RegistrarProvedor(pro);
            }


        [HttpPost]
        [Route("EstatusProveedor")]
        public dynamic EstatusProveedor([FromBody] int id)
        {

            return ProveedoresData.BajaProvedor(id);
        }


        [HttpPost]
        [Route("MostrarProveedores")]
        public dynamic EstatusProveedor()
        {

            return ProveedoresData.MostarProvedores();
        }

    }
    }

