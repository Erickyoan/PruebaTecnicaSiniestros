using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record SiniestroDto(
        Guid Id,
        DateTime FechaEvento,
        DateTime FechaReportada,
        string Departamento,
        string Ciudad,
        string Direccion,
        string TipoSiniestro,
        int NumeroVictimas,
        string? Descripcion,
        List<VehiculoDto> Vehiculos
    );

    public record VehiculoDto(string Placa, string Tipo);
}
