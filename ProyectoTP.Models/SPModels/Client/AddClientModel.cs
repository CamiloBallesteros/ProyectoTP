using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTP.Models.SPModels.Client
{
    public class AddClientModel
    {
        public string TipoDocumento { get; set; }
        public int NumeroDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string? NumeroCelular { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int CiudadId { get; set; }
    }
}
