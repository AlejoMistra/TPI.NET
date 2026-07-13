using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Domain;

namespace Data
{
    public class ProfesionalRepository : IProfesionalRepository
    {
        private static readonly List<Profesional> _profesionales = new List<Profesional>(); 
        private static int _nextId = 1;

        public Task AddAsync(Profesional profesional)
        {
            // Simula auto-incremento de ID
            profesional.SetId(_nextId++);

            // Asignar navigation property de la Especialidad
            var EspecialidadRepo = new EspecialidadRepository();
            var especialidad = EspecialidadRepo.GetAllSync().FirstOrDefault(e => e.Id == profesional._especialidadId);
            if (especialidad != null) { 
                profesional.SetEspecialidad(especialidad);
            }

            _profesionales.Add(profesional);
            return Task.CompletedTask;
        }

        public Task<Profesional?> GetByIdAsync(int id)
        {
            var profesional = _profesionales.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(profesional);
        }

        public Task<IEnumerable<Profesional>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Profesional>>(_profesionales.ToList());
        }

        public Task<Profesional?> UpdateAsync(Profesional profesional)
        {
            var existingProfesional = _profesionales.FirstOrDefault(p => p.Id == profesional.Id);
            if (existingProfesional != null)
            {
                // Actualizar propiedades
                existingProfesional.SetNombre(profesional.Nombre);
                existingProfesional.SetApellido(profesional.Apellido);
                existingProfesional.SetMatricula(profesional.Matricula);
                existingProfesional.SetEspecialidadId(profesional._especialidadId);

                // Actualizar navigation property de la Especialidad
                var EspecialidadRepo = new EspecialidadRepository();
                var especialidad = EspecialidadRepo.GetAllSync().FirstOrDefault(e => e.Id == profesional._especialidadId);
                if (especialidad != null) { 
                    existingProfesional.SetEspecialidad(especialidad);
                }
                return Task.FromResult<Profesional?>(existingProfesional);
            }
            return Task.FromResult<Profesional?>(null);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var profesional = _profesionales.FirstOrDefault(p => p.Id == id);
            if (profesional != null)
            {
                _profesionales.Remove(profesional);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }


    }
}
