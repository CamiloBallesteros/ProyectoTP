using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTP.Models.Tables
{
    public class Cliente
    {
        [DisplayName("Tipo Doc.")]
        [Required]
        public int TipoDocumento { get; set; }

        [DisplayName("Num. Documento")]
        [Required]
        [Key]
        public int NumeroDocumento { get; set; }

        [DisplayName("Nombre Completo")]
        [MaxLength(100, ErrorMessage = "El Nombre debe tener menos de 100 Caracteres")]
        [MinLength(6, ErrorMessage = "El Nombre debe tener mas de 6 Caracteres")]
        [Required]
        public string NombreCompleto { get; set; } = "";

        [DisplayName("Numero Celular")]
        public string? NumeroCelular { get; set; }
        
        [DisplayName("Fecha de Nacimiento")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime FechaNacimiento { get;set; }

        [NotMapped]
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

        [Required]
        public int CiudadId { get; set; }

        [ForeignKey("CiudadId")]
        public virtual Ciudad Ciudad { get; set; }

        public virtual ICollection<RegistroLlamada> Llamadas { get; set; }
    }
}
