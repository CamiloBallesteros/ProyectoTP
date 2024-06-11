using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoTP.BusinessLayer.Interfaces;
using ProyectoTP.Models.ViewModels;

namespace ProyectoTP.WebAPP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IClientService clientService)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
