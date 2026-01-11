using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Siniestro
    {
        public Guid Id { get; private set; }
        public DateTime FechaEvento { get; private set; }
        public DateTime FechaReportado { get; private set; }
        public string Departamento { get; private set; }
        public string Ciudad { get; private set; }
        public string Dirrecion { get; private set; }  
        public string TipoSiniestro { get; private set; }
        public int NumeroVictimas { get; private set; }
        public string? Descripcion { get; private set; }

        //Solo lectura hacia afuera
        private readonly List<Vehiculo> _vehiculos = new();
        public IReadOnlyCollection<Vehiculo> Vehiculos => _vehiculos.AsReadOnly();

        private Siniestro() { }

        public Siniestro(DateTime fechaEvento, DateTime fechaReportada, string departamento, string ciudad, string direccion, string tipoSiniestro, int numeroVictimas, string? descripcion)
        {
            if (fechaEvento > DateTime.Now)
                throw new ArgumentException("La fecha del siniestro no puede ser futura.");

            if (numeroVictimas < 0)
                throw new ArgumentException("El número de víctimas no puede ser negativo.");

            if(fechaReportada < fechaEvento)
                throw new ArgumentException("La fecha reportada no puede ser menor a la fecha del evento.");

            Id = Guid.NewGuid();
            FechaEvento = fechaEvento;
            FechaReportado = fechaReportada;
            Departamento = departamento;
            Ciudad = ciudad;
            Dirrecion = direccion;
            TipoSiniestro = tipoSiniestro;
            NumeroVictimas = numeroVictimas;
            Descripcion = descripcion;
        }

        public void AgregarVehiculo(string placa, string tipo, string? modelo)
        {
            
            var vehiculo = new Vehiculo(placa, tipo, modelo);
            _vehiculos.Add(vehiculo);
        }
    }
}
