using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class Bitacora
    {
        public int Id { get; set; }   
        public int IdUsuario { get; set; }   
        public string Detalles { get; set; }
        public DateTime Fecha { get; set; }
    }
}
