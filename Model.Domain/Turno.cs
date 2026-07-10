using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Domain
{
    public class Turno
    {

        public enum EstadosTurno{

            Pendiente, Confirmado, Atendido, Ausente, Cancelado, Reprogramado

        }

        public int idTurno { get; private set; }

        public DateTime fechaHoraInicio { get; private set; }

        public DateTime fechaHoraFin { get; private set; }

        public string motivo { get; private set; } = string.Empty;

        public EstadosTurno estadoTurno { get; private set; }

        public string observacion { get; private set; } = string.Empty;

        public Factura? factura_turno { get; private set; } 

        public int id_factura { get; private set; }

        public ConsultaMedica? consulta_medica { get; private set; }

        public int id_consulta_m {  get; private set; }

        public Profesional? profesional_turno { get; private set; }

        public int id_profesional_ {  get; private set; }
        public Paciente? paciente_turno { get; private set; }

        public int id_paciente {  get; private set; }

        public Administrativo? administrativo_turno { get; private set; }

        public int id_administrativo {  get; private set; }


    }
}