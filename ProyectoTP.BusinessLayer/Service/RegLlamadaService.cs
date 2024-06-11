using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoTP.BusinessLayer.Interfaces;
using ProyectoTP.DataLayer.Data;
using ProyectoTP.Models.SPModels.Client;
using ProyectoTP.Models.SPModels.RegCall;
using ProyectoTP.Models.SPReturns;

namespace ProyectoTP.BusinessLayer.Service
{
    public class RegLlamadaService : IRegLlamadaService
    {
        private readonly ApplicationDbContext _context;
        public RegLlamadaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public AddRegCallReturn AddRegistroLlamada(AddRegCallModel addRegCall)
        {
            var result = new AddRegCallReturn();
            try
            {
                var ret = _context.PRegisterNewCall(addRegCall);
                result.SuccessMessage = "Llamada registrada exitosamente";
                result.IdInserted = 0;
            }
            catch (SqlException ex)
            {
                result.ErrorFlag = true;
                if (ex.Message.Contains("FOREIGN KEY constraint \"FK_RegistroLlamadas_Clientes_ClienteId\""))
                    result.ErrorMessage = "El Cliente asociado a la llamada no esta registrado en el Sistema.";
                else
                    result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
