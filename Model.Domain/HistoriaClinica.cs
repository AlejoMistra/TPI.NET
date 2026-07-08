using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Domain
{
    public class HistoriaClinica
    {
        public int idHistoria { get; private set; }
        
        public string grupoSanguineo { get; private set; }
        
        public string alergias { get; private set; }
     
        public string antecedentes { get; private set; }
        
        public int fechaCreacion { get; private set; }
        
        }
    }
