using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.IdentityModel.Tokens;
using ProyectoTP.BusinessLayer.Interfaces;
using ProyectoTP.Models.SPModels.RegCall;
using ProyectoTP.Models.ViewModels;

namespace ProyectoTP.WebAPP.Pages.RegistroLlamadas
{
    public class RegistroModel : PageModel
    {
        private readonly ILogger<RegistroModel> _logger;
        private readonly IClientService _clientService;
        private readonly ICiudadService _ciudadService;
        private readonly IRegLlamadaService _regLlamadaService;

        public RegistroModel(ILogger<RegistroModel> logger, IClientService clientService, ICiudadService ciudadService, IRegLlamadaService regLlamadaService)
        {
            _logger = logger;
            _clientService = clientService;
            _ciudadService = ciudadService;
            _regLlamadaService = regLlamadaService;
        }

        [FromQuery]
        public int NumDocumento { get; set; } = 0;
        [FromQuery]
        public string TipoDocumento { get; set; } = "";

        public SelectList ListaCiudades { get; set; } = default!;

        [BindProperty]
        public ClientView Cliente { get; set; } = null!;

        [BindProperty]
        public AddRegCallModel RegCall { get; set; } = null!;
        public bool IsCliente { get; set; } = false;

        public IActionResult OnGet()
        {
            ListaCiudades = new SelectList(_ciudadService.GetCiudadesList());
            if (NumDocumento > 0 && TipoDocumento != "")
            {
                Cliente = _clientService.GetClientByTypeAndDoc(TipoDocumento, NumDocumento);
                if(Cliente.NumeroDocumento > 0)
                    IsCliente = true;

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
            return new JsonResult(new { 
                Ok = !result.ErrorFlag,
                Mensaje = result.ErrorFlag ? result.ErrorMessage : result.SuccessMessage,
                NumDocChanged = result.UpdatedClient
            });
        }

        public JsonResult OnPostRegistrarLlamada()
        {
            var result = _regLlamadaService.AddRegistroLlamada(RegCall);
            return new JsonResult(new
            {
                Ok = !result.ErrorFlag,
                Mensaje = result.ErrorFlag ? result.ErrorMessage : result.SuccessMessage
            });
        }

        
    }
}
