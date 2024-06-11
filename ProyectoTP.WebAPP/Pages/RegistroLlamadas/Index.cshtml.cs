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
        public string? StartDateString { get; set; } = null;
        [FromQuery]
        public string? EndDateString { get; set; } = null;
        public int PromedioEdad { get; set; } = 0;
        public int ClientesMenor_20 { get; set; } = 0;
        public int ClientesMayor_50 { get; set; } = 0;

        public List<LlamadasClienteView> ListaLlamadas { get; set; } = [];
        public SelectList ListaCiudades { get; set; } = default!;

        public IActionResult OnGet()
        {
            ListaCiudades = new SelectList(_ciudadService.GetCiudadesList());
            DateTime? startdate;
            DateTime? enddate;
            _ = DateTime.TryParse(StartDateString, out DateTime start);
            _ = DateTime.TryParse(EndDateString, out DateTime end);
            startdate = start < new DateTime(2000, 1, 1) ? null : start;
            enddate = end < new DateTime(2000, 1, 1) ? null : end;

            ListaLlamadas = _clientService.GetClientListFiltered(NumDocumento, NombreCiudad,startdate, enddate);
            if (ListaLlamadas.Count > 0)
            {
                PromedioEdad = (int)Math.Round(ListaLlamadas.Select(x => new { x.NumeroDocumento ,x.Edad}).Distinct().Average(x => x.Edad)!.Value);
                ClientesMenor_20 = ListaLlamadas.Select(x => new { x.NumeroDocumento, x.Edad }).Distinct().Count(x => x.Edad < 20);
                ClientesMayor_50 = ListaLlamadas.Select(x => new { x.NumeroDocumento, x.Edad }).Distinct().Count(x => x.Edad > 50);
            }
            return Page();
        }
    }
}
