using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoTP.BusinessLayer.Interfaces;
using ProyectoTP.Models.SPModels.RegCall;
using ProyectoTP.Models.ViewModels;
using ProyectoTP.WebAPP.Pages.RegistroLlamadas;

namespace ProyectoTP.WebAPP.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<RegistroModel> _logger;
        private readonly IClientService _clientService;
        private readonly ICiudadService _ciudadService;

        public IndexModel(ILogger<RegistroModel> logger, IClientService clientService, ICiudadService ciudadService)
        {
            _logger = logger;
            _clientService = clientService;
            _ciudadService = ciudadService;
        }
        [BindProperty]
        public ClientView Cliente { get; set; }
        [FromQuery]
        public int NumDocumento { get; set; } = 0;
        [FromQuery]
        public string TipoDocumento { get; set; } = "";
        public SelectList ListaCiudades { get; set; } = default!;

        public bool IsCliente { get; set; } = false;
        public IActionResult OnGet()
        {
            ListaCiudades = new SelectList(_ciudadService.GetCiudadesList());
            if (TipoDocumento != "" && NumDocumento > 0)
            {
                Cliente = _clientService.GetClientByTypeAndDoc(TipoDocumento, NumDocumento);
                if (Cliente.NumeroDocumento > 0)
                    IsCliente = true;
                else
                {
                    Cliente.NumeroDocumento = NumDocumento;
                    Cliente.TipoDocumento = TipoDocumento;
                }
            }
            return Page();
        }

        public JsonResult OnPostCreateClient()
        {
            var result = _clientService.AddClient(Cliente);
            return new JsonResult(new
            {
                Ok = !result.ErrorFlag,
                Mensaje = result.ErrorFlag ? result.ErrorMessage : result.SuccessMessage,
                NumDocChanged = result.ClientAdded
            });
        }

        public JsonResult OnPostUpdClient()
        {
            var result = _clientService.UpdateClient(Cliente.NumeroDocumento, Cliente.NumeroCelular, Cliente.NombreCiudad);
            return new JsonResult(new
            {
                Ok = !result.ErrorFlag,
                Mensaje = result.ErrorFlag ? result.ErrorMessage : result.SuccessMessage,
                NumDocChanged = result.UpdatedClient
            });
        }
    }
}
