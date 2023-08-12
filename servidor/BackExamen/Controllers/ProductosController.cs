using Microsoft.AspNetCore.Mvc;
using tenis.Data;
using tenis.Models;

namespace tenis.Controllers
{
    [ApiController]
    [Route("tenis")]
    public class ProductosController
    {

        [HttpPost]
        [Route("NuevoProducto")]
        public dynamic RegistarMateria([FromBody] Producto P)
        {
            return ProductoData.RegistrarProductoNuevo(P);
        }

        [HttpPost]
        [Route("MostrarProductosActivos")]
        public dynamic MostrarProductosActivos()
        {
            return ProductoData.MostrarProductosActivos();
        }

        [HttpPost]
        [Route("MostrarProductosInactivos")]
        public dynamic MostrarProductosInActivos()
        {
            return ProductoData.MostrarProductosInActivos();
        }

        [HttpPost]
        [Route("CambiarEstatusProducto")]
        public dynamic CambiaEstatus([FromBody] ProductoId id)
        {
            return ProductoData.CambiarEstatusProducto(id);
        }
    }
}
