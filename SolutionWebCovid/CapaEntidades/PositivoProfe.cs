using System;

namespace CapaEntidades
{
    public class PositivoProfe
    {
        public int Id_posProfe { get; set; }
        public DateTime FechaConfirmado { get; set; }
        public string Comprobacion { get; set; }
        public string Antecedentes { get; set; }
        public int NumContagio { get; set; }
        public string Extra { get; set; }
        public int F_Profe { get; set; }
        public string Riesgo { get; set; }
    }
}
