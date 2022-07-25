using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Cuatrimestre
    {
        public int id { set; get; }
        public string periodo { set; get; }
        public int anio { set; get; }
        public string fechaInicio { set; get; }
        public string fechaFin { set; get; }
        public string extra { set; get; }

        public string datosCuatrimestre()
        {
            return "Periodo: " + this.periodo + " Año: " + this.anio + " Fecha inicio: " +
                this.fechaInicio + " Fecha fin: " + this.fechaFin + " Extra: " + this.extra;
        }
    }
}
