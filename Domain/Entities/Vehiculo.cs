using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vehiculo
    {
        public Guid Id { get; private set; }
        public string Placa { get; private set; }
        public string Tipo { get; private set; } 
        public string? Modelo { get; private set; }

        private Vehiculo() { }

        
        public Vehiculo(string placa, string tipo, string? modelo)
        {
            if (string.IsNullOrWhiteSpace(placa))
                throw new ArgumentException("El dato de la placa es obligatoria", nameof(placa));

            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("El dato del tipo de vehículo es obligatorio", nameof(tipo));

            Id = Guid.NewGuid();
            Placa = placa.ToUpper().Trim(); 
            Tipo = tipo;
            Modelo = modelo;
        }
    }
}

