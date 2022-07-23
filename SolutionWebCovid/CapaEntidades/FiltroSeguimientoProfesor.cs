using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class FiltroSeguimientoProfesor
    {
        public int id_Segui { get; set; }
        public string Doctor { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public DateTime FechaSeguimiento { get; set; }
        public string FormComunica { get; set; }
        public string Reporte { get; set; }
        public string Entrevista { get; set; }
        public string Extra { get; set; }

    }
}
