using System;
using System.Collections.Generic;
using CapaAccesoDatos;
using System.Data;
using System.Data.SqlClient;
using CapaEntidades;

namespace CapaLogicaNegocios
{
    public class LogicaNegociosCuatrimestre
    {
        private AccesoDatos AccesoDatosSql = null;
        public LogicaNegociosCuatrimestre(string Cad1)
        {
            this.AccesoDatosSql = new AccesoDatos(Cad1);
        }

        // regla para consultar datos de todos los cuatrimestres
        public DataSet consultarCuatrimestres(ref string mensaje)
        {
            string query = "SELECT * FROM Cuatrimestre;";
            SqlParameter[] sqlParameters = null;
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para insertar un nuevo cuatrimestre
        public Boolean insertarCuatrimestre(Cuatrimestre cuatriNuevo, ref string mensaje)
        {
            string queryInsert = "INSERT INTO Cuatrimestre(Periodo,Anio,Inicio,Fin,Extra)" +
                "VALUES(@periodo,@anio,@inicio,@fin,@extra);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("periodo", SqlDbType.VarChar, 30),
                new SqlParameter("anio", SqlDbType.Int),
                new SqlParameter("inicio", SqlDbType.Date),
                new SqlParameter("fin", SqlDbType.Date),
                new SqlParameter("extra", SqlDbType.VarChar, 50),
            };
            sqlParameters[0].Value = cuatriNuevo.periodo;
            sqlParameters[1].Value = cuatriNuevo.anio;
            sqlParameters[2].Value = cuatriNuevo.fechaInicio;
            sqlParameters[3].Value = cuatriNuevo.fechaFin;
            sqlParameters[4].Value = cuatriNuevo.extra;
            Boolean result = AccesoDatosSql.Modificar(queryInsert, sqlParameters, ref mensaje);
            return result;
        }

        // regla para editar datos de un cuatrimestre
        public Boolean editarCuatrimestre(int idCuatri, Cuatrimestre cuatriActualizado, ref string mensaje)
        {
            string queryUpdate = "UPDATE Cuatrimestre SET Periodo=@periodo,Anio=@anio,Inicio=@inicio," +
                "Fin=@fin,Extra=@extra WHERE id_Cuatrimestre=@idCuatri;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idCuatri", SqlDbType.SmallInt),
                new SqlParameter("periodo", SqlDbType.VarChar, 30),
                new SqlParameter("anio", SqlDbType.Int),
                new SqlParameter("inicio", SqlDbType.Date),
                new SqlParameter("fin", SqlDbType.Date),
                new SqlParameter("extra", SqlDbType.VarChar, 50),
            };
            sqlParameters[0].Value = cuatriActualizado.id;
            sqlParameters[1].Value = cuatriActualizado.periodo;
            sqlParameters[2].Value = cuatriActualizado.anio;
            sqlParameters[3].Value = cuatriActualizado.fechaInicio;
            sqlParameters[4].Value = cuatriActualizado.fechaFin;
            sqlParameters[5].Value = cuatriActualizado.extra;
            Boolean result = AccesoDatosSql.Modificar(queryUpdate, sqlParameters, ref mensaje);
            return result;
        }

        // regla para eliminar un cuatrimestre
        public Boolean eliminarCuatrimestre(int idCuatri, ref string mensaje)
        {
            Boolean result = false;
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idCuatri", SqlDbType.SmallInt)
            };
            sqlParameters[0].Value = idCuatri;

            // verificar que no existan dependencias
            string queryDependence1 = "SELECT * FROM GrupoCuatrimestre WHERE F_Cuatri=@idCuatri;";
            SqlDataReader resultDependence1 = AccesoDatosSql.ConsultarReader(queryDependence1, sqlParameters, ref mensaje);
            if (resultDependence1.HasRows)
            {
                // eliminar GrupoCuatrimestre
                string queryDeleteGrupoCuatri = "DELETE FROM GrupoCuatrimestre WHERE F_Cuatri=@idCuatri;";
                AccesoDatosSql.Modificar(queryDeleteGrupoCuatri, sqlParameters, ref mensaje);
                // eliminar Cuatrimestre
                string queryDeleteCuatri = "DELETE FROM Cuatrimestre WHERE id_Cuatrimestre=@idCuatri";
                result = AccesoDatosSql.Modificar(queryDeleteCuatri, sqlParameters, ref mensaje);
            }
            else
            {
                string queryDeleteCuatri = "DELETE FROM Cuatrimestre WHERE id_Cuatrimestre=@idCuatri";
                result = AccesoDatosSql.Modificar(queryDeleteCuatri, sqlParameters, ref mensaje);
            }
            return result;
        }
    }
}
