using CapaAccesoDatos;
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

        //public bool ModificarSqlSinSpamConexion(string querySql, SqlParameter[] sqlParameters, ref string msg)
        //{
        //    return this.AccesoDatosSql.ModificarSinCerrar(querySql, sqlParameters, ref msg);
        //}

        //public string ModificarSql(string querySql, SqlParameter[] sqlParameters, ref string msg)
        //{
        //    string result = "";
        //    this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref msg);
        //    return result;
        //}

        //public DataSet ConsultaDsSQL(string querySql, ref string msg)
        //{
        //    SqlParameter[] sqlParameters = null;
        //    return this.AccesoDatosSql.ConsultaDS(querySql, sqlParameters, ref msg);
        //}
    }
}
