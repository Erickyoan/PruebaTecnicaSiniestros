using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISiniestroRepository
    {
        // Escritura
        Task AddAsync(Siniestro siniestro);

        
        Task<(IEnumerable<Siniestro> Items, int TotalCount)> GetByFilterAsync(
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string? departamento,
            int page,
            int pageSize);
    }
}
