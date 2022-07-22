using CapaAccesoDatos;
using CapaEntidades;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaLogicaNegocios
{
    public class LogicaNegocios
    {
        private AccesoDatos AccesoDatosSql = null;
        public LogicaNegocios(string Cad1, string Cad2)
        {
            this.AccesoDatosSql = new AccesoDatos(Cad2);
        }

    }
}
