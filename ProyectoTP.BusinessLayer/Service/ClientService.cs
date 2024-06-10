using Microsoft.IdentityModel.Tokens;
using ProyectoTP.BusinessLayer.Helpers;
using ProyectoTP.BusinessLayer.Interfaces;
using ProyectoTP.DataLayer.Data;
using ProyectoTP.DataLayer.Tables;
using ProyectoTP.Models.SPReturns;
using ProyectoTP.Models.ViewModels;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProyectoTP.BusinessLayer.Service
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;
        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public AddClientReturn AddClient()
        {
            throw new NotImplementedException();
        }

        public ClientView GetClientByTypeAndDoc(string tipoDoc, int numeroDocumento)
        {
            var cliente = new ClientView();
            if(!tipoDoc.IsNullOrEmpty() && numeroDocumento > 0)
            {
                var result = _context.Clientes.Where(c => c.TipoDocumento == tipoDoc && c.NumeroDocumento == numeroDocumento).FirstOrDefault();
                if(result != null)
                {
                    cliente = cliente.Map(result);
                }
            }
            return cliente;
        }

        public List<ClientView> GetClientListFiltered(int numeroDocumento, string ciudad, DateTime? startDate = null, DateTime? endDate = null)
        {
            var listaCliente = new List<ClientView>();
            IQueryable<RegistroLlamada> registros = _context.RegistroLlamadas.Include(r => r.Cliente).ThenInclude(c => c.Ciudad).AsQueryable();
            if (numeroDocumento > 0)
                registros = registros.Where(c => c.Cliente.NumeroDocumento.ToString().StartsWith(numeroDocumento.ToString()));

            if (!ciudad.IsNullOrEmpty())
                registros = registros.Where(c => c.Cliente.Ciudad.Nombre.StartsWith(ciudad, StringComparison.CurrentCultureIgnoreCase));

            if(startDate != null && startDate.Value < DateTime.Now)
                registros = registros.Where(c => c.FechaLlamada > startDate.Value);

            if (endDate != null && endDate.Value < DateTime.Now)
                registros = registros.Where(c => c.FechaLlamada < endDate.Value);

            listaCliente = registros.Select(c => (new ClientView() { NombreCiudad = c.Cliente.Ciudad.Nombre}).Map(c.Cliente)).ToList();
            return listaCliente;
        }

        public UpdateClientReturn UpdateClient()
        {
            throw new NotImplementedException();
        }
    }
}
