using Microsoft.EntityFrameworkCore;
using ProyectoTP.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTP.DataLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<RegistroLlamada> RegistroLlamadas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasIndex(p => p.Nombre).IsUnique();
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(p => p.FechaNacimiento)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<RegistroLlamada>(entity =>
            {
                entity.Property(p => p.FechaLlamada)
                    .HasColumnType("datetime");
            });
        }
    }
}
