namespace tenis.Models
{
    public class Provedor
    {
        public string Nombre { get; set; }
        public string MateriaPrima { get; set; }
        public float Costo { get; set; }
    }

    public class Provedore
    {
        public int idProveedor { get; set; }

        public string? Nombre { get; set; }
        public string? MateriaPrima { get; set; }
        public double Costo { get; set; }
        public int Estatus { get; set; }
    }
}
