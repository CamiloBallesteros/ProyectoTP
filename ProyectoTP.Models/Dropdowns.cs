using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoTP.Models
{
    public static class Dropdowns
    {
        public static SelectList TipoDocSelect { get; set; } =
        new SelectList(
            new List<(string, string)>() {
                ("CC","Cedula de Ciudadanía"),
                ("CE","Cedula de Extranjería"),
                ("TI","Tarjeta de Identidad")
            }.Select(x => new SelectListItem() { Text = x.Item2, Value = x.Item1 }), "Value", "Text");
        public static SelectList TipoSolicitud { get; set; } = new SelectList( new string[] { "Petición", "Queja", "Reclamo", "Sugerencia"});
    }
}
