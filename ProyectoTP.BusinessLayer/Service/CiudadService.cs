using ProyectoTP.BusinessLayer.Interfaces;
using ProyectoTP.DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTP.BusinessLayer.Service
{
    public class CiudadService : ICiudadService
    {
        private readonly ApplicationDbContext _context;
        public CiudadService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<string> GetCiudadesList()
        {
            return _context.Ciudades.Select(c => c.Nombre).ToList();
        }
    }
}
