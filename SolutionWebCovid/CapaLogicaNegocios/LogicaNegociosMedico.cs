using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaAccesoDatos;
using CapaEntidades;

namespace CapaLogicaNegocios
{
    public class LogicaNegociosMedico
    {
        private AccesoDatos AccesoDatosSql = null; //objeto acceso a datos
        public LogicaNegociosMedico(string Cad)
        {
            this.AccesoDatosSql = new AccesoDatos(Cad);
        }

        //----------------------------------------------Medico-------------------------------------------------
        
        // regla para consultar datos de todos los medicos
        public DataSet consultarMedicos(ref string mensaje)
        {
            string query = "SELECT * FROM Medico;";
            SqlParameter[] sqlParameters = null;
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para insertar un nuevo medico
        public Boolean insertarMedico(Medico medico, ref string mensaje)
        {
            string queryInsert = "INSERT INTO Medico(Nombre,App,Apm,Telefono,correo,horario,especialidad,extra)" +
                "VALUES(@nombre,@app,@apm,@tel,@correo,@horario,@especialidad,@extra);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("nombre", medico.nombre),
                new SqlParameter("app", medico.app),
                new SqlParameter("apm", medico.apm),
                new SqlParameter("tel", medico.telefono),
                new SqlParameter("correo", medico.correo),
                new SqlParameter("horario", medico.horario),
                new SqlParameter("especialidad", medico.especialidad),
                new SqlParameter("extra", medico.extra)
            };
            return AccesoDatosSql.Modificar(queryInsert, sqlParameters, ref mensaje);
        }

        // regla para obtener datos de un medico
        public Medico buscarAlumno(int idMedico, ref string mensaje)
        {
            Medico medico = null;
            string query = "SELECT * FROM Medico WHERE ID_Dr=@idMedico;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idMedico", idMedico)
            };
            DataSet dataMedico = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataMedico != null)
            {
                medico = new Medico();
                foreach (DataRow row in dataMedico.Tables[0].Rows)
                {
                    medico.id = (int)row[0];
                    medico.app = (string)row[1];
                    medico.apm = (string)row[2];
                    medico.telefono = (string)row[3];
                    medico.correo = (string)row[4];
                    medico.horario = (string)row[5];
                    medico.especialidad = (string)row[6];
                    medico.extra = (string)row[7];
                }
            }
            return medico;
        }

        // regla para editar datos de un medico
        public Boolean editarMedico(int idMedico, Medico medico, ref string mensaje)
        {
            string queryUpdate = "UPDATE Medico SET Nombre=@nom,App=@app,Apm=@apm,Telefono=@tel," +
                "correo=@correo,horario=@horario,especialidad=@especialidad,extra=@extra ID_Dr=@idMedico;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idMedico", idMedico),
                new SqlParameter("nom", medico.nombre),
                new SqlParameter("app", medico.app),
                new SqlParameter("apm", medico.apm),
                new SqlParameter("tel", medico.telefono),
                new SqlParameter("correo", medico.correo),
                new SqlParameter("horario", medico.horario),
                new SqlParameter("especialidad", medico.especialidad),
                new SqlParameter("extra", medico.extra)
            };
            return AccesoDatosSql.Modificar(queryUpdate, sqlParameters, ref mensaje);
        }

        // regla para eliminar un medico
        public Boolean eliminarMedico(int idMedico, ref string mensaje)
        {
            Boolean result = false;
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idMedico", idMedico)
            };

            // verificar que no existan dependencias
            string queryDependence1 = "SELECT * FROM SeguimientoAL WHERE F_medico=@idMedico;";
            string queryDependence2 = "SELECT * FROM SeguimientoPRO WHERE F_medico=@idMedico;";
            SqlDataReader resultDependence1 = AccesoDatosSql.ConsultarReader(queryDependence1, sqlParameters, ref mensaje);
            SqlDataReader resultDependence2 = AccesoDatosSql.ConsultarReader(queryDependence2, sqlParameters, ref mensaje);
            if (resultDependence1.HasRows || resultDependence2.HasRows)
            {
                // eliminar SeguimientoAlumno
                string queryDeleteSegAlumno = "DELETE SeguimientoAL WHERE F_medico=@idMedico;";
                AccesoDatosSql.Modificar(queryDeleteSegAlumno, sqlParameters, ref mensaje);
                // eliminar SeguimientoProfe
                string queryDeleteSegProfe = "DELETE SeguimientoPRO WHERE F_medico=@idMedico;";
                AccesoDatosSql.Modificar(queryDeleteSegProfe, sqlParameters, ref mensaje);
                // eliminar Medico
                string queryDeleteMedico = "DELETE Medico WHERE ID_Dr=@idMedico";
                result = AccesoDatosSql.Modificar(queryDeleteMedico, sqlParameters, ref mensaje);
            }
            else
            {
                string queryDeleteMedico = "DELETE Medico WHERE ID_Dr=@idMedico";
                result = AccesoDatosSql.Modificar(queryDeleteMedico, sqlParameters, ref mensaje);
            }
            return result;
        }

        // regla para obtener colección de medicos en ListItems
        public List<Medico> obtenerListaMedicos(ref string mensaje)
        {
            List<Medico> listMedicos = null;
            string query = "SELECT * FROM Medico;";
            SqlParameter[] sqlParameters = null;
            DataSet dataMedicos = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataMedicos != null)
            {
                listMedicos = new List<Medico>();
                foreach (DataRow row in dataMedicos.Tables[0].Rows)
                {
                    listMedicos.Add(new Medico()
                    {
                        id = (int)row[0],
                        nombre = (string)row[1],
                        app = (string)row[2],
                        apm = (string)row[3],
                        telefono = (string)row[4],
                        correo = (string)row[5],
                        horario = (string)row[6],
                        especialidad = (string)row[7],
                        extra = (string)row[8]
                    });
                }
            }
            return listMedicos;
        }

        // regla para obtener colección de medicos en ListItems para listbox
        public List<Medico> obtenerColeccionMedicos(ref string mensaje)
        {
            List<Medico> listMedicos = null;
            string query = "SELECT ID_Dr,Nombre+' '+App+' '+Apm as NombreMedico,Especialidad FROM Medico;";
            SqlParameter[] sqlParameters = null;
            DataSet dataMedicos = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataMedicos != null)
            {
                listMedicos = new List<Medico>();
                foreach (DataRow row in dataMedicos.Tables[0].Rows)
                {
                    listMedicos.Add(new Medico()
                    {
                        id = (int)row[0],
                        nombre = (string)row[1],
                        especialidad = (string)row[2]
                    });
                }
            }
            return listMedicos;
        }
    }
}
