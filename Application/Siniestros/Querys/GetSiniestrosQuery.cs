using Application.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Siniestros.Querys
{
    
    public class GetSiniestrosQuery : IRequest<PagedResult<SiniestroDto>>
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Departamento { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    
    public record PagedResult<T>(IEnumerable<T> Items, int TotalCount, int PageNumber, int PageSize);

    
    public class GetSiniestrosQueryHandler : IRequestHandler<GetSiniestrosQuery, PagedResult<SiniestroDto>>
    {
        private readonly ISiniestroRepository _repository;

        public GetSiniestrosQueryHandler(ISiniestroRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<SiniestroDto>> Handle(GetSiniestrosQuery request, CancellationToken cancellationToken)
        {
            // llmado al repositorio
            var (items, totalCount) = await _repository.GetByFilterAsync(
                request.FechaInicio,
                request.FechaFin,
                request.Departamento,
                request.PageNumber,
                request.PageSize
            );

          // mapeo 
            var dtos = items.Select(s => new SiniestroDto(
                s.Id,
                s.FechaEvento,
                s.FechaReportado,
                s.Departamento,
                s.Ciudad,
                s.Dirrecion,
                s.TipoSiniestro,
                s.NumeroVictimas,
                s.Descripcion,
                s.Vehiculos.Select(v => new VehiculoDto(v.Placa, v.Tipo)).ToList()
            ));

            return new PagedResult<SiniestroDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
