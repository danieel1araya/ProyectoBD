using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class Rol
    {
        public int Id { get; set; }  
        public string NombreRol { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return $"{NombreRol} - {Descripcion}";
        }

    }
}
