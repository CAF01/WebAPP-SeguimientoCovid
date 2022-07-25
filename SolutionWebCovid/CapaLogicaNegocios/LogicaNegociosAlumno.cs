using System;
using System.Collections.Generic;
using CapaAccesoDatos;
using System.Data;
using System.Data.SqlClient;
using CapaEntidades;

namespace CapaLogicaNegocios
{
    public class LogicaNegociosAlumno
    {
        private AccesoDatos AccesoDatosSql = null; //objeto acceso a datos
        public LogicaNegociosAlumno(string Cad1)
        {
            this.AccesoDatosSql = new AccesoDatos(Cad1);
        }

        // regla para consultar datos de todos los alumnos
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
            string queryInsert = "INSERT INTO Alumno(Matricula,Nombre,Ap_pat,Ap_mat,Genero,Correo,Celular,F_EdoCivil)" +
                "VALUES(@matricula,@nom,@app,@apm,@genero,@correo,@cel,@fedocivil);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("matricula", SqlDbType.VarChar, 20),
                new SqlParameter("nom", SqlDbType.VarChar, 150),
                new SqlParameter("app", SqlDbType.VarChar, 100),
                new SqlParameter("apm", SqlDbType.VarChar, 100),
                new SqlParameter("genero", SqlDbType.VarChar, 10),
                new SqlParameter("correo", SqlDbType.VarChar, 200),
                new SqlParameter("cel", SqlDbType.VarChar, 20),
                new SqlParameter("fedocivil", SqlDbType.TinyInt),
            };
            sqlParameters[0].Value = alumnoNuevo.matricula;
            sqlParameters[1].Value = alumnoNuevo.nombre;
            sqlParameters[2].Value = alumnoNuevo.apPat;
            sqlParameters[3].Value = alumnoNuevo.apMat;
            sqlParameters[4].Value = alumnoNuevo.genero;
            sqlParameters[5].Value = alumnoNuevo.correo;
            sqlParameters[6].Value = alumnoNuevo.celular;
            sqlParameters[7].Value = alumnoNuevo.f_edoCivil;
            Boolean result = AccesoDatosSql.Modificar(queryInsert, sqlParameters, ref mensaje);
            return result;
        }

        // regla para obtener datos de un alumno
        public Alumno recuperarAlumno(int idAlumno, ref string mensaje)
        {
            Alumno resultAlumno = new Alumno();
            string query = "SELECT * FROM Alumno WHERE ID_Alumno=@idAlumno;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idAlumno", SqlDbType.Int)
            };
            sqlParameters[0].Value = idAlumno;
            DataSet dataAlumno = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataAlumno != null)
            {
                foreach (DataRow row in dataAlumno.Tables[0].Rows)
                {
                    // asignar al objeto a devolver, los datos recuperados
                    resultAlumno.id = (int)row[0];
                    resultAlumno.matricula = (string)row[1];
                    resultAlumno.nombre = (string)row[2];
                    resultAlumno.apPat = (string)row[3];
                    resultAlumno.apMat = (string)row[4];
                    resultAlumno.genero = (string)row[5];
                    resultAlumno.correo = (string)row[6];
                    resultAlumno.celular = (string)row[7];
                    resultAlumno.f_edoCivil = (int)row[8];
                }
            }
            return resultAlumno;
        }

        // regla para editar datos de un alumno
        public Boolean editarAlumno(int idAlumno, Alumno alumnoActualizado, ref string mensaje)
        {
            string queryUpdate = "UPDATE Alumno SET Matricula=@matricula,Nombre=@nom,Ap_pat=@app,Ap_mat=@apm," +
                "Genero=@genero,Correo=@correo,Celular=@cel,F_EdoCivil=@fedocivil" +
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
                new SqlParameter("fedocivil", SqlDbType.TinyInt),
            };
            sqlParameters[0].Value = idAlumno;
            sqlParameters[1].Value = alumnoActualizado.matricula;
            sqlParameters[2].Value = alumnoActualizado.nombre;
            sqlParameters[3].Value = alumnoActualizado.apPat;
            sqlParameters[4].Value = alumnoActualizado.apMat;
            sqlParameters[5].Value = alumnoActualizado.genero;
            sqlParameters[6].Value = alumnoActualizado.correo;
            sqlParameters[7].Value = alumnoActualizado.celular;
            sqlParameters[8].Value = alumnoActualizado.f_edoCivil;
            Boolean result = AccesoDatosSql.Modificar(queryUpdate, sqlParameters, ref mensaje);
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

        // regla para obtener colección de alumnos en ListItems
        public List<Alumno> obtenerColeccionAlumnos(ref string mensaje)
        {
            List<Alumno> listAlumnos = new List<Alumno>();
            string query = "SELECT ID_Alumno,Matricula,Nombre+' '+Ap_pat+' '+Ap_mat as NombreCompleto FROM Alumno;";
            SqlParameter[] sqlParameters = null;
            DataSet dataAlumnos = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataAlumnos != null)
            {
                foreach(DataRow row in dataAlumnos.Tables[0].Rows)
                {
                    listAlumnos.Add(new Alumno()
                    {
                        id = (int)row[0],
                        matricula = (string)row[1],
                        nombre = (string)row[2]
                    });
                }
            }
            return listAlumnos;
        }
    }
}
