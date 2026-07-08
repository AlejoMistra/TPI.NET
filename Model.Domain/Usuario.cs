using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    internal class Usuario
    {
        private enum Rol
        {
            Admin,
            Administrativo,
            Paciente,
            Profesional
        }

        private enum Estado
        {
            Activo,
            Inactivo
        }

        public int Id { get; private set; }

        public string Nombre { get; private set; }

        public string contraseña { get; private set; }

        public Rol Rol { get; private set; }

        public Estado Estado { get; private set; }
    }
}
