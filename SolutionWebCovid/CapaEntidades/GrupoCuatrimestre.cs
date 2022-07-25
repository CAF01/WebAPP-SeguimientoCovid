using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class GrupoCuatrimestre
    {
        public int id { set; get; }
        public int f_progEdu { set; get; }
        public int f_grupo { set; get; }
        public int f_cuatri { set; get; }
        public string turno { set; get; }
        public string modalidad { set; get; }
        public string extra { set; get; }

        public string datosGrupoCuatrimestre()
        {
            return " Programa educativo: " + this.f_progEdu + " Grupo: " + this.f_grupo + " Cuatrimestre: " +
                this.f_cuatri + " Turno: " + this.turno + " Modalidad: " + this.modalidad + " Extra: " + this.extra;
        }
    }
}
