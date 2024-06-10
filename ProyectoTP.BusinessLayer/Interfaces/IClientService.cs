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
        public AddClientReturn AddClient();
        public UpdateClientReturn UpdateClient();
        public ClientView GetClientByTypeAndDoc(string tipoDoc, int numeroDocumento);
        public List<ClientView> GetClientListFiltered(int numeroDocumento, string ciudad, DateTime? startDate = null, DateTime? endDate = null);
    }
}
