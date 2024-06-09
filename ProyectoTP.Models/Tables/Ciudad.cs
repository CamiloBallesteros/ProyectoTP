using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTP.Models.Tables
{
    public class Ciudad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
