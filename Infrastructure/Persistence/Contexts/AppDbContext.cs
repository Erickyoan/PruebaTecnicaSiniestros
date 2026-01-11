using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Siniestro> Siniestros { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Siniestro>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Configuración importante DDD
                
                var navigation = entity.Metadata.FindNavigation(nameof(Siniestro.Vehiculos));
                navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

                // Relación 1:N
                entity.HasMany(s => s.Vehiculos)
                      .WithOne()
                      .HasForeignKey("SiniestroId") 
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Vehiculo
            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Placa).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(20);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
