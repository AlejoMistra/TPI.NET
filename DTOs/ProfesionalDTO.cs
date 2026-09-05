using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProfesionalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NroDocumento { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public int EspecialidadId { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string Estado { get; set; } = "Activo";
    }
}
