using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTP.Models.SPReturns
{
    public class AddRegCallReturn : GenericReturn
    {
        public int IdInserted { get; set; }
        public DateTime FechaDeLlamada { get; set; }
    }
}
