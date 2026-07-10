using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Domain
{
    public class HistoriaClinica
    {
        public enum GrupoSanguineo { 
            
            A_POSITIVO,
            A_NEGATIVO,
            B_POSITIVO,
            B_NEGATIVO,
            AB_POSITIVO,
            AB_NEGATIVO,
            O_POSITIVO,
            O_NEGATIVO
            }
        
        public int idHistoria { get; private set; }

        public GrupoSanguineo grupo_sang { get; private set; }
        public ICollection <string> alergias { get; private set; }
     
        public ICollection<string> antecedentes { get; private set; }
        
        public DateTime fechaCreacion { get; private set; }

        public HistoriaClinica()
        {
            alergias = new List<string>();
            antecedentes = new List<string>();
            fechaCreacion = DateTime.Now;
            grupo_sang = grupo_sang;

        }
        
        }
    }
