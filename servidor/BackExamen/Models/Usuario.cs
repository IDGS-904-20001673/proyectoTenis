namespace serverTenis.Models
{
    public class Usuario
    {
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Contrasenia { get; set; }
        public int? IdRole { get; set; } 
        public Domicilio? Domicilio { get; set; }
    }
}
