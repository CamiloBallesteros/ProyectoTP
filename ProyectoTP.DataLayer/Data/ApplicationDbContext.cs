using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoTP.DataLayer.Tables;
using ProyectoTP.Models.SPModels.Client;
using ProyectoTP.Models.SPModels.RegCall;
using System.Data;

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

        public int PAddClient(AddClientModel newClient)
        {
            var tipoDocParam = new SqlParameter("@TipoDoc", (object)newClient.TipoDocumento ?? DBNull.Value);
            var numDocParam = new SqlParameter("@NumeroDoc", (object)newClient.NumeroDocumento ?? DBNull.Value);
            var ciudadIdParam = new SqlParameter("@CiudadID", (object)newClient.CiudadId ?? DBNull.Value);
            var nombreParam = new SqlParameter("@Nombre", (object)newClient.NombreCompleto ?? DBNull.Value);
            var fechaNacimientoParam = new SqlParameter("@FechaNacimiento", (object)newClient.FechaNacimiento ?? DBNull.Value);
            var numCelularParam = new SqlParameter("@NumCelular", (object)newClient.NumeroCelular ?? DBNull.Value);

            return Database.ExecuteSqlRaw("EXEC dbo.SPAddClient @TipoDoc, @NumeroDoc, @CiudadID, @Nombre, @FechaNacimiento, @NumCelular",
                tipoDocParam, numDocParam, ciudadIdParam, nombreParam, fechaNacimientoParam, numCelularParam
            );
        }

        public int PUpdateClient(UpdClientModel updatedClient)
        {
            var numDocParam = new SqlParameter("@NumeroDoc", (object)updatedClient.NumeroDocumento ?? DBNull.Value);
            var ciudadIdParam = new SqlParameter("@CiudadID", (object)updatedClient.CiudadId ?? DBNull.Value);
            var numCelularParam = new SqlParameter("@NumCelular", (object)updatedClient.NumeroCelular ?? DBNull.Value);

            return Database.ExecuteSqlRaw("EXEC dbo.SPUpdateClient @NumeroDoc, @CiudadID, @NumCelular",
                numDocParam, ciudadIdParam, numCelularParam
            );
        }

        public int PRegisterNewCall(AddRegCallModel newRegCall)
        {
            var clienteIdParam = new SqlParameter("@ClienteID", (object)newRegCall.ClienteId ?? DBNull.Value);
            var razonParam = new SqlParameter("@Razon", (object)newRegCall.Razon ?? DBNull.Value);

            return Database.ExecuteSqlRaw("EXEC dbo.SPRegisterCall @ClienteID, @Razon",
                clienteIdParam, razonParam
            );
        }


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
