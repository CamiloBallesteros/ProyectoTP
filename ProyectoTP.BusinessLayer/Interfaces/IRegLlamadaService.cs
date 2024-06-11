using ProyectoTP.Models.SPModels.RegCall;
using ProyectoTP.Models.SPReturns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTP.BusinessLayer.Interfaces
{
    public interface IRegLlamadaService
    {
        public AddRegCallReturn AddRegistroLlamada(AddRegCallModel addRegCall);
    }
}
