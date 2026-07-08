using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Domain
{
    public class ConsultaMedica
    {
        public int idConsultaMedica { get; private set; }

        public string sintomas { get; private set; }

        public string notasClinicas { get; private set; }

        public string diagnostico { get; private set; }

    }
}