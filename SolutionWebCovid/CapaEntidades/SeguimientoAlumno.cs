using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class SeguimientoAlumno
    {
        public int id { set; get; }
        public int f_positivoAlum { set; get; }
        public int f_medico { set; get; }
        public string fecha { set; get; }
        public string formaComunicacion { set; get; }
        public string reporte { set; get; }
        public string entrevista { set; get; }
        public string extra { set; get; }
    }
}
