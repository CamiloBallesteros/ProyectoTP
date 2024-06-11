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
using ProyectoTP.Models.SPModels.Client;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Data.SqlTypes;

namespace ProyectoTP.BusinessLayer.Service
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;
        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public AddClientReturn AddClient(ClientView newClient)
        {
            var result = new AddClientReturn();
            var addClientModel = new AddClientModel().Map(newClient);
            addClientModel.CiudadId = _context.Ciudades.FirstOrDefault(c => c.Nombre == newClient.NombreCiudad)!.Id;
            try
            {
                _context.PAddClient(addClientModel);
                result.SuccessMessage = "Cliente Agregado exitosamente";
                result.ClientAdded = addClientModel.NumeroDocumento;
            }
            catch (SqlException ex)
            {
                result.ErrorFlag = true;
                if (ex.Message.Contains("FOREIGN KEY constraint \"FK_Clientes_Ciudades_CiudadId\""))
                    result.ErrorMessage = "El Cliente a ingresar no esta asociado a una Ciudad registrada en el Sistema.";
                else if (ex.Message.Contains("PRIMARY KEY constraint 'PK_Clientes'."))
                    result.ErrorMessage = "El Cliente a ingresar ya esta registrado en el Sistema.";
                else
                    result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public ClientView GetClientByTypeAndDoc(string tipoDoc, int numeroDocumento)
        {
            var cliente = new ClientView();
            if (!tipoDoc.IsNullOrEmpty() && numeroDocumento > 0)
            {
                var result = _context.Clientes.Include(c => c.Ciudad).Where(c => c.TipoDocumento == tipoDoc && c.NumeroDocumento == numeroDocumento).FirstOrDefault();
                if (result != null)
                {
                    cliente = cliente.Map(result);
                    cliente.NombreCiudad = result.Ciudad.Nombre;
                }
            }
            return cliente;
        }

        public List<LlamadasClienteView> GetClientListFiltered(int numeroDocumento, string ciudad, DateTime? startDate = null, DateTime? endDate = null)
        {
            var listaCliente = new List<LlamadasClienteView>();
            var isFilter = false;
            IQueryable<RegistroLlamada> registros = _context.RegistroLlamadas.Include(r => r.Cliente).ThenInclude(c => c.Ciudad).AsQueryable();
            if (numeroDocumento > 0 && numeroDocumento.ToString().Length > 2)
            {
                registros = registros.Where(c => c.Cliente.NumeroDocumento.ToString().StartsWith(numeroDocumento.ToString()));
                isFilter = true;
            }

            if (!ciudad.IsNullOrEmpty())
            {
                registros = registros.Where(c => c.Cliente.Ciudad.Nombre.StartsWith(ciudad));
                isFilter = true;
            }

            if (startDate != null && startDate.Value < DateTime.Now)
            {
                registros = registros.Where(c => c.FechaLlamada > startDate.Value);
                isFilter = true;
            }

            if (endDate != null && endDate.Value < DateTime.Now)
            {
                registros = registros.Where(c => c.FechaLlamada < endDate.Value);
                isFilter = true;
            }

            listaCliente = isFilter ? registros.Select(c =>
                            new LlamadasClienteView()
                            {
                                NombreCiudad = c.Cliente.Ciudad.Nombre,
                                FechaLlamada = c.FechaLlamada!.Value
                            }.Map(c.Cliente)).ToList() 
                       : new List<LlamadasClienteView>() ;


            return listaCliente;
        }

        public UpdateClientReturn UpdateClient(int numDocumento, string? numCelular = null, string? nombreCiudad = null)
        {
            var result = new UpdateClientReturn();
            var updClientModel = new UpdClientModel()
            {
                NumeroDocumento = numDocumento,
                CiudadId = _context.Ciudades.FirstOrDefault(c => c.Nombre == nombreCiudad)?.Id,
                NumeroCelular = numCelular
            };
            try
            {
                _context.PUpdateClient(updClientModel);
                result.SuccessMessage = "Cliente Actualizado exitosamente";
                result.UpdatedClient = updClientModel.NumeroDocumento;
                result.NewCelNumber = updClientModel.NumeroCelular;
                result.NewCity = nombreCiudad;
            }
            catch (SqlException ex)
            {
                result.ErrorFlag = true;
                if (ex.Message.Contains("FOREIGN KEY constraint \"FK_Clientes_Ciudades_CiudadId\""))
                    result.ErrorMessage = "El Campo Ciudad Seleccionado no existe en el Sistema.";
                else
                    result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
