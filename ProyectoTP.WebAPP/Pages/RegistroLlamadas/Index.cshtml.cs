using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoTP.BusinessLayer.Interfaces;
using ProyectoTP.Models.ViewModels;

namespace ProyectoTP.WebAPP.Pages.RegistroLlamadas
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IClientService _clientService;
        private readonly ICiudadService _ciudadService;

        public IndexModel(ILogger<IndexModel> logger, IClientService clientService, ICiudadService ciudadService)
        {
            _logger = logger;
            _clientService = clientService;
            _ciudadService = ciudadService;
        }

        [FromQuery]
        public int NumDocumento { get; set; } = 0;
        [FromQuery]
        public string NombreCiudad { get; set; } = "";
        [FromQuery]
        public DateTime? StartDate { get; set; } = null;
        [FromQuery]
        public DateTime? EndDate { get; set; } = null;
        public int PromedioEdad { get; set; } = 0;
        public int ClientesMenor_20 { get; set; } = 0;
        public int ClientesMayor_50 { get; set; } = 0;

        public List<LlamadasClienteView> ListaLlamadas { get; set; } = [];
        public SelectList ListaCiudades { get; set; } = default!;

        public IActionResult OnGet()
        {
            ListaCiudades = new SelectList(_ciudadService.GetCiudadesList());
            ListaLlamadas = _clientService.GetClientListFiltered(NumDocumento, NombreCiudad, StartDate, EndDate);
            if (ListaLlamadas.Count > 0)
            {
                PromedioEdad = (int)Math.Round(ListaLlamadas.Average(x => x.Edad)!.Value);
                ClientesMenor_20 = ListaLlamadas.Count(x => x.Edad < 20);
                ClientesMayor_50 = ListaLlamadas.Count(x => x.Edad > 50);
            }
            return Page();
        }
    }
}
