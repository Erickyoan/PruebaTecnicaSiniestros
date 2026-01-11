using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class SiniestroRepository : ISiniestroRepository
    {
        private readonly AppDbContext _context;

        public SiniestroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Siniestro siniestro)
        {
            await _context.Siniestros.AddAsync(siniestro);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Siniestro> Items, int TotalCount)> GetByFilterAsync(
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string? departamento,
            int pageNumber,
            int pageSize)
        {
            // Iniciamos la query
            var query = _context.Siniestros
                .Include(s => s.Vehiculos)
                .AsNoTracking()
                .AsQueryable();

            // filtros dinamicos (SQL Where)
            if (fechaInicio.HasValue)
                query = query.Where(s => s.FechaEvento >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(s => s.FechaEvento <= fechaFin.Value);

            if (!string.IsNullOrWhiteSpace(departamento))
                query = query.Where(s => s.Departamento.Contains(departamento));

            // Contamos el total para la paginacion 
            var totalCount = await query.CountAsync();

            // Aplicamos Paginacion y ordenamos por fecha descendente
            var items = await query
                .OrderByDescending(s => s.FechaEvento)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

       
        public Task<Siniestro?> GetByIdAsync(Guid id)
        {
            return _context.Siniestros
                .Include(s => s.Vehiculos)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
