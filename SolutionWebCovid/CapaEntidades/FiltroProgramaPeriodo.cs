using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class FiltroProgramaPeriodo
    {
        public string ProgramaEd { get; set; }
        public int id_Cuatrimestre { get; set; }
        public string Periodo { get; set; }
        public string Anio { get; set; }
        public int RegistroEmpleado { get; set; }
        public string Profesor { get; set; }
        public int Id_posProfe { get; set; }
        public DateTime FechaConfirmado { get; set; }

    }
}
