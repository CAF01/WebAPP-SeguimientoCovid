using System;
using CapaAccesoDatos;
using System.Data;
using System.Data.SqlClient;
using CapaEntidades;

namespace CapaLogicaNegocios
{
    public class LogicaNegociosAlumno
    {
        private AccesoDatos AccesoDatosSql = null;
        public LogicaNegociosAlumno(string Cad1, string Cad2)
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

        // regla para consultar datos todos los alumnos
        public DataSet consultarAlumnos(ref string mensaje)
        {
            string query = "SELECT * FROM Alumno;";
            SqlParameter[] sqlParameters = null;
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para insertar un nuevo alumno
        public Boolean insertarAlumno(Alumno alumnoNuevo, ref string mensaje)
        {
            string query = "INSERT INTO Alumno(Matricula,Nombre,Ap_pat,Ap_mat,Genero,Correo,Celular,F_EdoCivil,F_Nivel)" +
                "VALUES(@matricula,@nom,@app,@apm,@genero,@correo,@cel,@fedocivil,@fnivel);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("matricula", SqlDbType.VarChar, 20),
                new SqlParameter("nom", SqlDbType.VarChar, 150),
                new SqlParameter("app", SqlDbType.VarChar, 100),
                new SqlParameter("apm", SqlDbType.VarChar, 100),
                new SqlParameter("genero", SqlDbType.VarChar, 10),
                new SqlParameter("correo", SqlDbType.VarChar, 200),
                new SqlParameter("cel", SqlDbType.VarChar, 20),
                new SqlParameter("fedocivil", SqlDbType.Int),
                new SqlParameter("fnivel", SqlDbType.Int)
            };
            sqlParameters[0].Value = alumnoNuevo.matricula;
            sqlParameters[1].Value = alumnoNuevo.nombre;
            sqlParameters[2].Value = alumnoNuevo.apPat;
            sqlParameters[3].Value = alumnoNuevo.apMat;
            sqlParameters[4].Value = alumnoNuevo.genero;
            sqlParameters[5].Value = alumnoNuevo.correo;
            sqlParameters[6].Value = alumnoNuevo.celular;
            sqlParameters[7].Value = alumnoNuevo.edoCivil;
            sqlParameters[8].Value = alumnoNuevo.nivel;
            Boolean result = AccesoDatosSql.Modificar(query, sqlParameters, ref mensaje);
            return result;
        }

        // regla para editar datos de un alumno
        public Boolean editarAlumno(int idAlumno, Alumno alumnoActualizado, ref string mensaje)
        {
            string query = "UPDATE Alumno SET Matricula=@matricula,Nombre=@nom,Ap_pat=@app,Ap_mat=@apm," +
                "Genero=@genero,Correo=@correo,Celular=@cel,F_EdoCivil=@fedocivil,F_Nivel=@fnivel" +
                "WHERE ID_Alumno=@idAlumno;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idAlumno", SqlDbType.Int),
                new SqlParameter("matricula", SqlDbType.VarChar, 20),
                new SqlParameter("nom", SqlDbType.VarChar, 150),
                new SqlParameter("app", SqlDbType.VarChar, 100),
                new SqlParameter("apm", SqlDbType.VarChar, 100),
                new SqlParameter("genero", SqlDbType.VarChar, 10),
                new SqlParameter("correo", SqlDbType.VarChar, 200),
                new SqlParameter("cel", SqlDbType.VarChar, 20),
                new SqlParameter("fedocivil", SqlDbType.Int),
                new SqlParameter("fnivel", SqlDbType.Int)
            };
            sqlParameters[0].Value = idAlumno;
            sqlParameters[1].Value = alumnoActualizado.matricula;
            sqlParameters[2].Value = alumnoActualizado.nombre;
            sqlParameters[3].Value = alumnoActualizado.apPat;
            sqlParameters[4].Value = alumnoActualizado.apMat;
            sqlParameters[5].Value = alumnoActualizado.genero;
            sqlParameters[6].Value = alumnoActualizado.correo;
            sqlParameters[7].Value = alumnoActualizado.celular;
            sqlParameters[8].Value = alumnoActualizado.edoCivil;
            sqlParameters[9].Value = alumnoActualizado.nivel;
            Boolean result = AccesoDatosSql.Modificar(query, sqlParameters, ref mensaje);
            return result;
        }

        // regla para eliminar un alumno
        public Boolean eliminarAlumno(int idAlumno, ref string mensaje)
        {
            Boolean result = false;
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idAlumno", SqlDbType.Int)
            };
            sqlParameters[0].Value = idAlumno;

            // verificar que no existan dependencias
            string queryDependence1 = "SELECT * FROM AlumnoGrupo WHERE F_Alumn=@idAlumno;";
            string queryDependence2 = "SELECT * FROM PositivoAlumno WHERE F_Alumno=@idAlumno;";
            SqlDataReader resultDependence1 = AccesoDatosSql.ConsultarReader(queryDependence1, sqlParameters, ref mensaje);
            SqlDataReader resultDependence2 = AccesoDatosSql.ConsultarReader(queryDependence2, sqlParameters, ref mensaje);
            if (resultDependence1.HasRows || resultDependence2.HasRows)
            {
                // eliminar AlumnoGrupo
                string queryDeleteAlumnoGrupo = "DELETE FROM AlumnoGrupo WHERE F_Alumn =@idAlumno;";
                AccesoDatosSql.Modificar(queryDeleteAlumnoGrupo, sqlParameters, ref mensaje);
                // eliminar PositivoAlumno
                string queryDeletePositivo = "DELETE FROM PositivoAlumno WHERE F_Alumno=@idAlumno;";
                AccesoDatosSql.Modificar(queryDeletePositivo, sqlParameters, ref mensaje);
                // eliminar Alumno
                string queryDeleteAlumno = "DELETE FROM Alumno WHERE ID_Alumno=@idAlumno;";
                result = AccesoDatosSql.Modificar(queryDeleteAlumno, sqlParameters, ref mensaje);
            }
            else
            {
                string queryDeleteAlumno = "DELETE FROM Alumno WHERE ID_Alumno=@idAlumno;";
                result = AccesoDatosSql.Modificar(queryDeleteAlumno, sqlParameters, ref mensaje);
            }
            return result;
        }
    }
}
