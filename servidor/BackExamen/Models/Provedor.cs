namespace tenis.Models
{
    public class Provedor
    {
        public string Nombre { get; set; }
        public string? Telefono { get; set; }
    }

    public class Provedore 
    {
        public int idProveedor { get; set; }

        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public int Estatus { get; set; }
    }

    public class IdModel
    {
        public int Id { get; set; }
    }
}
