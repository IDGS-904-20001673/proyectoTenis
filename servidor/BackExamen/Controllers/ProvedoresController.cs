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
        [Route("RegistrarProvedor")]
        public dynamic RegistarProveedor([FromBody] Provedor pro)
        {

                return ProveedoresData.RegistrarProvedor(pro);
            }



        [HttpPost]
        [Route("CambiarEstatusProveedor")]
        public dynamic EstatusProveedor([FromBody] IdModel id)
        {

            return ProveedoresData.BajaProvedor(id);
        }


        [HttpPost]
        [Route("MostrarTodosLosProveedores")]
        public dynamic Proveedores()
        {

            return ProveedoresData.MostarTodosProvedores();
        }

        [HttpPost]
        [Route("MostrarProveedoresActivos")]
        public dynamic ProveedoresAct()
        {

            return ProveedoresData.MostarProvedoresActivos();
        }

    }


}

