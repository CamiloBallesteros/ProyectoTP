using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoTP.BusinessLayer.Interfaces;
using ProyectoTP.Models.ViewModels;

namespace ProyectoTP.WebAPP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IClientService _clientService;

        [FromQuery]
        public string? consultar { get; set; } = null!;

        public List<ClientView> listaClientes { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        public void OnGet()
        {
            listaClientes = _clientService.GetClientListFiltered(108,"");
        }
    }
}
