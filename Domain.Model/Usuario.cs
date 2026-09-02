using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    internal class Usuario
    {
        public enum Roles
        {
            Administrativo,
            Paciente,
            Profesional
        }

        public enum Estados
        {
            Activo,
            Inactivo
        }

        public int Id { get; private set; }

        public string Nombre { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public Roles Rol { get; private set; } 

        public Estados Estado { get; private set; }

        public int PersonaId { get; private set; }
        public Persona Persona { get; private set; } = null!;
    }
}
