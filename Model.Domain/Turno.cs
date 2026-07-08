using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Domain
{
    public class Turno
    {

        public int idTurno { get; private set; }

        public DateTime fechaHoraInicio { get; private set; }

        public DateTime fechaHoraFin { get; private set; }

        public string motivo { get; private set; }

        public string estadoTurno { get; private set; }

        public string observacion { get; private set; }
    }
}