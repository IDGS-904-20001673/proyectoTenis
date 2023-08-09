namespace tenis.Models
{
    public class Producto
    {
        public string? Nombre { get; set; }
        public double Precio { get; set; }
        public string? Descripcion { get; set; }
        public string? ImageName { get; set; }
    }

    public class ProductoE
    {
        public int idProducto { get; set; }
        public string? nombre { get; set; }
        public double precio { get; set; }
        public string? descripccion { get; set; }
        public string? image_name { get; set; }
        public int estatus { get; set; }
    }


}
