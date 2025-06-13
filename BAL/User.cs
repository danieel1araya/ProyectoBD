using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class User
    {
        public int Id { get; set; }

        public string Usuario { get; set; }

        public string Contrasena { get; set; } 

        public int Activo { get; set; }

        public string EstadoTexto => Activo == 1 ? "Activo" : "Inactivo";
    }
}
