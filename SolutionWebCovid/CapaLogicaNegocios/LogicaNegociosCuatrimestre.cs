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
        public LogicaNegociosCuatrimestre(string Cad)
        {
            this.AccesoDatosSql = new AccesoDatos(Cad);
        }

        // ----------------------------------------Cuatrimestre-----------------------------------------------

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
        public Boolean insertarCuatrimestre(Cuatrimestre cuatrimestre, ref string mensaje)
        {
            string queryInsert = "INSERT INTO Cuatrimestre(Periodo,Anio,Inicio,Fin,Extra)" +
                "VALUES(@periodo,@anio,@inicio,@fin,@extra);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("periodo", cuatrimestre.periodo),
                new SqlParameter("anio", cuatrimestre.anio),
                new SqlParameter("inicio", cuatrimestre.fechaInicio),
                new SqlParameter("fin", cuatrimestre.fechaFin),
                new SqlParameter("extra", cuatrimestre.extra)
            };
            return AccesoDatosSql.Modificar(queryInsert, sqlParameters, ref mensaje);
        }

        // regla para obtener datos de un cuatrimestre
        public Cuatrimestre buscarCuatrimestre(int idCuatrimestre, ref string mensaje)
        {
            Cuatrimestre cuatrimestre = null;
            string query = "SELECT * FROM Cuatrimestre WHERE Id_Cuatrimestre=@idCuatri;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idCuatri", idCuatrimestre)
            };
            DataSet dataCuatrimestre = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataCuatrimestre != null)
            {
                cuatrimestre = new Cuatrimestre();
                foreach (DataRow row in dataCuatrimestre.Tables[0].Rows)
                {
                    // asignar al objeto a devolver, los datos recuperados
                    cuatrimestre.id = (int)row[0];
                    cuatrimestre.periodo = (string)row[1];
                    cuatrimestre.anio = (int)row[2];
                    cuatrimestre.fechaInicio = (string)row[3];
                    cuatrimestre.fechaFin = (string)row[4];
                    cuatrimestre.extra = (string)row[5];
                }
            }
            return cuatrimestre;
        }

        // regla para editar datos de un cuatrimestre
        public Boolean editarCuatrimestre(int idCuatrimestre, Cuatrimestre cuatrimestre, ref string mensaje)
        {
            string queryUpdate = "UPDATE Cuatrimestre SET Periodo=@periodo,Anio=@anio,Inicio=@inicio," +
                "Fin=@fin,Extra=@extra WHERE id_Cuatrimestre=@idCuatri;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idCuatri", idCuatrimestre),
                new SqlParameter("periodo", cuatrimestre.periodo),
                new SqlParameter("anio", cuatrimestre.anio),
                new SqlParameter("inicio", cuatrimestre.fechaInicio),
                new SqlParameter("fin", cuatrimestre.fechaFin),
                new SqlParameter("extra", cuatrimestre.extra)
            };
            return AccesoDatosSql.Modificar(queryUpdate, sqlParameters, ref mensaje);
        }

        // regla para eliminar un cuatrimestre
        public Boolean eliminarCuatrimestre(int idCuatrimestre, ref string mensaje)
        {
            Boolean result = false;
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idCuatri", idCuatrimestre)
            };

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

        // regla para obtener colección de cuatrimestres en ListItems
        public List<Cuatrimestre> obtenerListaCuatrimestres(ref string mensaje)
        {
            List<Cuatrimestre> listCuatrimestres = null;
            string query = "SELECT * FROM Cuatrimestre;";
            SqlParameter[] sqlParameters = null;
            DataSet dataCuatrimestres = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataCuatrimestres != null)
            {
                listCuatrimestres = new List<Cuatrimestre>();
                foreach (DataRow row in dataCuatrimestres.Tables[0].Rows)
                {
                    listCuatrimestres.Add(new Cuatrimestre()
                    {
                        id = (int)row[0],
                        periodo = (string)row[1],
                        anio = (int)row[2],
                        fechaInicio = (string)row[2],
                        fechaFin = (string)row[3],
                        extra = (string)row[4]
                    });
                }
            }
            return listCuatrimestres;
        }

        // regla para obtener colección de cuatrimestres en ListItems para listbox
        public List<Cuatrimestre> obtenerColeccionCuatrimestres(ref string mensaje)
        {
            List<Cuatrimestre> listCuatrimestres = null;
            string query = "SELECT id_Cuatrimestre,Periodo,Anio FROM Cuatrimestre;";
            SqlParameter[] sqlParameters = null;
            DataSet dataCuatrimestres = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataCuatrimestres != null)
            {
                listCuatrimestres = new List<Cuatrimestre>();
                foreach (DataRow row in dataCuatrimestres.Tables[0].Rows)
                {
                    listCuatrimestres.Add(new Cuatrimestre()
                    {
                        id = (int)row[0],
                        periodo = (string)row[1],
                        anio = (int)row[2]
                    });
                }
            }
            return listCuatrimestres;
        }

        //---------------------------------------GrupoCuatrimestre---------------------------------------------

        // regla para consultar datos de todos los grupo_cuatrimestre
        public DataSet consultarGruposCuatrimestres(ref string mensaje)
        {
            string query = "SELECT * FROM GrupoCuatrimestre;";
            SqlParameter[] sqlParameters = null;
            DataSet result = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (result != null) { return result; }
            return null;
        }

        // regla para insertar un nuevo grupo_cuatrimestre
        public Boolean insertarGrupoCuatrimestre(GrupoCuatrimestre grupocuatri, ref string mensaje)
        {
            string queryInsert = "INSERT INTO GrupoCuatrimestre(F_ProgEd,F_Grupo,F_Cuatri,Turno,Modalidad,Extra)" +
                "VALUES(@f_progEdu,@f_grupo,@f_cuatri,@turno,@modalidad,@extra);";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("f_progEdu", grupocuatri.f_progEdu),
                new SqlParameter("f_grupo", grupocuatri.f_grupo),
                new SqlParameter("f_cuatri", grupocuatri.f_cuatri),
                new SqlParameter("turno", grupocuatri.turno),
                new SqlParameter("modalidad", grupocuatri.modalidad),
                new SqlParameter("extra", grupocuatri.extra)
            };
            return AccesoDatosSql.Modificar(queryInsert, sqlParameters, ref mensaje);
        }

        // regla para obtener datos de un grupo_cuatrimestre
        public GrupoCuatrimestre buscarGrupoCuatrimestre(int idGrupoCuatri, ref string mensaje)
        {
            GrupoCuatrimestre grupocuatri = null;
            string query = "SELECT * FROM GrupoCuatrimestre WHERE Id_GruCuat=@idGruCuat;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idGruCuat", idGrupoCuatri)
            };
            DataSet dataGrupoCuatri = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataGrupoCuatri != null)
            {
                grupocuatri = new GrupoCuatrimestre();
                foreach (DataRow row in dataGrupoCuatri.Tables[0].Rows)
                {
                    // asignar al objeto a devolver, los datos recuperados
                    grupocuatri.id = (int)row[0];
                    grupocuatri.f_progEdu = (int)row[1];
                    grupocuatri.f_grupo = (int)row[2];
                    grupocuatri.f_cuatri = (int)row[3];
                    grupocuatri.turno = (string)row[4];
                    grupocuatri.modalidad = (string)row[5];
                    grupocuatri.extra = (string)row[6];
                }
            }
            return grupocuatri;
        }

        // regla para editar datos de un grupo_cuatrimestre
        public Boolean editarGrupoCuatrimestre(int idGrupoCuatri, GrupoCuatrimestre grupocuatri, ref string mensaje)
        {
            string queryUpdate = "UPDATE GrupoCuatrimestre SET F_ProgEd=@f_progEdu,F_Grupo=f_grupo,F_Cuatri=f_cuatri," +
                "Turno=@turno,Modalidad=@modalidad,Extra=@extra WHERE Id_GruCuat=@idGrupoCuatri;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idGrupoCuatri", idGrupoCuatri),
                new SqlParameter("f_progEdu", grupocuatri.f_progEdu),
                new SqlParameter("f_grupo", grupocuatri.f_grupo),
                new SqlParameter("f_cuatri", grupocuatri.f_cuatri),
                new SqlParameter("turno", grupocuatri.turno),
                new SqlParameter("modalidad", grupocuatri.modalidad),
                new SqlParameter("extra", grupocuatri.extra)
            };
            return AccesoDatosSql.Modificar(queryUpdate, sqlParameters, ref mensaje);
        }

        // regla para eliminar un grupo_cuatrimestre
        public Boolean eliminarGrupoCuatrimestre(int idGrupoCuatri, ref string mensaje)
        {
            Boolean result = false;
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("idGruCuat", idGrupoCuatri)
            };
            // verificar que no existan dependencias
            string queryDependence1 = "SELECT * FROM ProfeGRupo WHERE F_GruCuat=@idGruCuat;";
            SqlDataReader resultDependence1 = AccesoDatosSql.ConsultarReader(queryDependence1, sqlParameters, ref mensaje);
            if (resultDependence1.HasRows)
            {
                // eliminar ProfeGrupo
                string queryDeleteGrupoCuatri = "DELETE FROM ProfeGRupo WHERE F_GruCuat=@idGruCuat;";
                AccesoDatosSql.Modificar(queryDeleteGrupoCuatri, sqlParameters, ref mensaje);
                // eliminar GrupoCuatrimestre
                string queryDeleteCuatri = "DELETE FROM GrupoCuatrimestre WHERE Id_GruCuat=@idGruCuat; ";
                result = AccesoDatosSql.Modificar(queryDeleteCuatri, sqlParameters, ref mensaje);
            }
            else
            {
                string queryDeleteCuatri = "DELETE FROM GrupoCuatrimestre WHERE Id_GruCuat=@idGruCuat; ";
                result = AccesoDatosSql.Modificar(queryDeleteCuatri, sqlParameters, ref mensaje);
            }
            return result;
        }

        // regla para obtener colección de grupo_cuatrimestre en ListItems
        public List<GrupoCuatrimestre> obtenerListaGrupoCuatri(ref string mensaje)
        {
            List<GrupoCuatrimestre> listGrupoCuatri = null;
            string query = "SELECT * FROM GrupoCuatrimestre;";
            SqlParameter[] sqlParameters = null;
            DataSet dataAlumnos = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataAlumnos != null)
            {
                listGrupoCuatri = new List<GrupoCuatrimestre>();
                foreach (DataRow row in dataAlumnos.Tables[0].Rows)
                {
                    listGrupoCuatri.Add(new GrupoCuatrimestre()
                    {
                        id = (int)row[0],
                        f_progEdu = (int)row[1],
                        f_grupo = (int)row[2],
                        f_cuatri = (int)row[3],
                        turno = (string)row[4],
                        modalidad = (string)row[5],
                        extra = (string)row[6]
                    });
                }
            }
            return listGrupoCuatri;
        }

        // regla para obtener colección de grupo_cuatrimestre en ListItems para listbox
        public List<GrupoCuatrimestre> obtenerColeccionGrupoCuatri(ref string mensaje)
        {
            List<GrupoCuatrimestre> listGrupoCuatri = null;
            string query = "SELECT GC.Id_GruCuat,  PE.ProgramaEd, cast(G.Grado as varchar) + G.Letra as Grupo, C.Periodo" +
                "FROM GrupoCuatrimestre GC JOIN ProgramaEducativo PE ON GC.F_ProgEd = PE.Id_pe" +
                "JOIN Grupo G ON GC.F_Grupo = G.Id_grupo JOIN Cuatrimestre C ON GC.F_Cuatri = C.id_Cuatrimestre;";
            SqlParameter[] sqlParameters = null;
            DataSet dataAlumnos = AccesoDatosSql.ConsultaDS(query, sqlParameters, ref mensaje);
            if (dataAlumnos != null)
            {
                listGrupoCuatri = new List<GrupoCuatrimestre>();
                foreach (DataRow row in dataAlumnos.Tables[0].Rows)
                {
                    listGrupoCuatri.Add(new GrupoCuatrimestre()
                    {
                        id = (int)row[0],
                        f_progEdu = (int)row[1],
                        f_grupo = (int)row[2],
                        f_cuatri = (int)row[3]
                    });
                }
            }
            return listGrupoCuatri;
        }
    }
}
