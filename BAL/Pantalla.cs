using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class Pantalla
    {
        public int Id { get; set; }
        public int IdSistema { get; set; }
        public string NombreSistema { get; set; }  // Nueva propiedad
        public string NombrePantalla { get; set; }

        public override string ToString()
        {
            return NombrePantalla;
        }
    }



}
