using Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IReporteRepository
    {
        Task<List<Turno>> GetTurnosParaReporteAsync(
            DateTime desde,
            DateTime hasta,
            int? profesionalId,
            int? especialidadId);

        Task<List<Factura>> GetFacturasEnPeriodoAsync(
            DateTime desde,
            DateTime hasta,
            int? profesionalId);
    }
}

