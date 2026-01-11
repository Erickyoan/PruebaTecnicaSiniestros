using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Siniestros.Commands
{
    public class CreateSiniestroCommand : IRequest<Guid>
    {
        public DateTime FechaEvento { get; set; }
        public DateTime FechaReportada { get; set; }
        public string Departamento { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string TipoSiniestro { get; set; } = string.Empty;
        public int NumeroVictimas { get; set; }
        public string? Descripcion { get; set; }

        // Lista simple de vehículos para registrar al mismo tiempo
        public List<VehiculoInputDto> Vehiculos { get; set; } = new();
    }

    public class VehiculoInputDto
    {
        public string Placa { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string? Modelo {  get; set; }
    }

   
    public class CreateSiniestroHandler : IRequestHandler<CreateSiniestroCommand, Guid>
    {
        private readonly ISiniestroRepository _repository;

        public CreateSiniestroHandler(ISiniestroRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateSiniestroCommand request, CancellationToken cancellationToken)
        {
            
            var siniestro = new Siniestro(
                request.FechaEvento,
                request.FechaReportada,
                request.Departamento,
                request.Ciudad,
                request.Direccion,
                request.TipoSiniestro,
                request.NumeroVictimas,
                request.Descripcion
            );

            
            foreach (var v in request.Vehiculos)
            {
                siniestro.AgregarVehiculo(v.Placa, v.Tipo, v.Modelo);
            }

            
            await _repository.AddAsync(siniestro);

            return siniestro.Id;
        }
    }
}
