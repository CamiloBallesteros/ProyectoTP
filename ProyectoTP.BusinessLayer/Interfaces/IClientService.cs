using ProyectoTP.Models.SPReturns;
using ProyectoTP.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTP.BusinessLayer.Interfaces
{
    public interface IClientService
    {
        public AddClientReturn AddClient(ClientView newClient);
        public UpdateClientReturn UpdateClient(int numDocumento, string? numCelular = null, string? nombreCiudad = null);
        public ClientView GetClientByTypeAndDoc(string tipoDoc, int numeroDocumento);
        public List<LlamadasClienteView> GetClientListFiltered(int numeroDocumento, string ciudad, DateTime? startDate = null, DateTime? endDate = null);
    }
}
