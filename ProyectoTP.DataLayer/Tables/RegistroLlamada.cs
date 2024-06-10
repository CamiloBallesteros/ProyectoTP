using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTP.DataLayer.Tables
{
    public class RegistroLlamada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(1000, ErrorMessage = "La Razón debe tener máximo 1000 Caracteres")]
        public string? Razon { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DataType(DataType.DateTime)]
        public DateTime? FechaLlamada { get; set; }

        [Required]
        public int ClienteId { get; set; }
        
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }
    }
}
