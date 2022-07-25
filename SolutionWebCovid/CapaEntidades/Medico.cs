using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Medico
    {
        public int id { set; get; }
        public string nombre { set; get; }
        public string app { set; get; }
        public string apm { set; get; }
        public string telefono { set; get; }
        public string correo { set; get; }
        public string horario { set; get; }
        public string especialidad { set; get; }
        public string extra { set; get; }
    }
}
