using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTP.Models.SPReturns
{
    public class UpdateClientReturn : GenericReturn
    {
        public int UpdatedClient { get; set; }
        public string? NewCity { get;set; }
        public string? NewCelNumber { get; set; }
    }
}
