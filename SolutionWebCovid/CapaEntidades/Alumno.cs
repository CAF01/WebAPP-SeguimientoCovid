using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Alumno
    {
        public int id = 0;
        public string matricula = "";
        public string nombre = "";
        public string apPat = "";
        public string apMat = "";
        public string genero = "";
        public string correo = "";
        public string celular = "";
        public int edoCivil = 0;
        public int nivel = 0;

        public string datosAlumno()
        {
            return "ID: " + this.id + " Matrícula: " + this.matricula + " Nombre: " + this.nombre +
                " Apellido paterno: " + this.apPat + " Apellido materno: " + apMat + " Género: " +
                this.genero + " Correo: " + this.correo + " Celular: " + this.celular + 
                " Estado civil: " + this.edoCivil + " Nivel: " + this.nivel;
        }
    }
}
