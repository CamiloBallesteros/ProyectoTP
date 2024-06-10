using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTP.Models.SPReturns
{
    public class GenericReturn
    {
        public bool ErrorFlag { get; set; } = false;
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
