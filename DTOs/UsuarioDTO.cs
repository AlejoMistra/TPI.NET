namespace DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int? PersonaId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }

    public class UsuarioCreateDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int? PersonaId { get; set; }
    }

    public class UsuarioUpdateDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int? PersonaId { get; set; }
        public string? Password { get; set; } // Opcional para actualizar
        public bool Activo { get; set; } = true;
    }
}