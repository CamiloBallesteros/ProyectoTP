using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTP.Models.ViewModels
{
    public class ClientView
    {
        public string TipoDocumento { get; set; }
        public int NumeroDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string? NumeroCelular { get; set; }
        public string? FechaNacimientoString { get; set; }
        
        public DateTime FechaNacimiento
        {
            get
            {
                if(DateTime.TryParse(FechaNacimientoString, out DateTime fechaNac))
                {
                    return fechaNac;
                }
                else
                {
                    return new DateTime();
                }
            }
        }
        public int? Edad
        {
            get
            {
                int edad = DateTime.Today.Year - FechaNacimiento.Year;
                if (DateTime.Now.DayOfYear < FechaNacimiento.DayOfYear)
                    edad -= 1;

                return edad;
            }
        }
        public string NombreCiudad { get; set; }
    }
}
