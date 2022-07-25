using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Alumno
    {
        public int id { get; set; }
        public string matricula { get; set; }
        public string nombre { get; set;}
        public string apPat { get; set; }
        public string apMat { get; set; }
        public string genero { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
        public int f_edoCivil { get; set; }

        public string datosAlumno()
        {
            return "ID: " + this.id + " Matrícula: " + this.matricula + " Nombre: " + this.nombre +
                " Apellido paterno: " + this.apPat + " Apellido materno: " + apMat + " Género: " +
                this.genero + " Correo: " + this.correo + " Celular: " + this.celular + 
                " Estado civil: " + this.f_edoCivil;
        }
    }
}
