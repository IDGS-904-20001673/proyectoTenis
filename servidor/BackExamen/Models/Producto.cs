﻿namespace tenis.Models
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

    public class ProductoId
    {
        public int idProducto { get; set; }
    }

    public class ProductoDetaPuntos
    {
        public int idProducto { get; set; }
        public double punto { get; set; }
    }


    public class ProductoDetaPuntosIds
    {
        public int idProductoDetalle { get; set; }
        public int idProducto { get; set; }
        public double punto { get; set; }
        public int cantidad { get; set; }
    }

    public class ProductoMateriaDetallePuntos
    {
        public int idDetalleMateriaProducto { get; set; }
        public int idProductoDetalle { get; set; }
        public int materiaPrimaId { get; set; }
        public double cantidadUsoMateria { get; set; }
    }



}
