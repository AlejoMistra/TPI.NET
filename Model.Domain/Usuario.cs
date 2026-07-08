using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    internal class Usuario
    {
        public enum Roles
        {
            Admin,
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

        public string Nombre { get; private set; }

        public string Contraseña { get; private set; }

        public Roles Rol { get; private set; }

        public Estados Estado { get; private set; }
    }
}
