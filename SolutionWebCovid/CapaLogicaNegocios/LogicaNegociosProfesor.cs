using CapaAccesoDatos;
using CapaEntidades;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CapaLogicaNegocios
{
    public class LogicaNegociosProfesor
    {
        private AccesoDatos AccesoDatosSql = null;
        public LogicaNegociosProfesor(string Cad)
        {
            this.AccesoDatosSql = new AccesoDatos(Cad);
        }

        /*Métodos para profesor*/
        public bool AgregarProfesor(Profesor profesor)
        {
            string querySql = "INSERT INTO Profesor (RegistroEmpleado,Nombre,Ap_pat,Ap_Mat,Genero,Categoria,Correo,Celular,F_EdoCivil)" +
                "VALUES (@RegEm,@Nom,@APP,@APM,@Gen,@Cat,@mail,@Cel,@Fed)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("RegEm",profesor.RegistroEmpleado),
                new SqlParameter("Nom",profesor.Nombre),
                new SqlParameter("APP",profesor.ap_pat),
                new SqlParameter("APM",profesor.ap_mat),
                new SqlParameter("Gen",profesor.Genero),
                new SqlParameter("Cat",profesor.Categoria),
                new SqlParameter("mail",profesor.Correo),
                new SqlParameter("Cel",profesor.Celular),
                new SqlParameter("Fed",profesor.F_EdoCivil)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }
        public bool ActualizarProfesor(Profesor profesor)
        {
            string querySql = "UPDATE Profesor SET RegistroEmpleado=@RegEm,Nombre=@Nom,ap_pat=@APP,ap_mat=@APM,Genero=@Gen,Categoria=@Cat," +
                "Correo=@mail,Celular=@Cel,F_EdoCivil=@Fed WHERE ID_Profe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",profesor.ID_Profe),
                new SqlParameter("RegEm",profesor.RegistroEmpleado),
                new SqlParameter("Nom",profesor.Nombre),
                new SqlParameter("APP",profesor.ap_pat),
                new SqlParameter("APM",profesor.ap_mat),
                new SqlParameter("Gen",profesor.Genero),
                new SqlParameter("Cat",profesor.Categoria),
                new SqlParameter("mail",profesor.Correo),
                new SqlParameter("Cel",profesor.Celular),
                new SqlParameter("Fed",profesor.F_EdoCivil)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public Profesor BuscarProfesor(int IdProfesor)
        {
            Profesor profesor = null;
            string querySql = "SELECT * FROM PROFESOR WHERE ID_Profe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",IdProfesor)
            };
            SqlDataReader reader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (reader != null && reader.HasRows)
            {
                profesor = new Profesor();
                profesor.ID_Profe = (int)reader[0];
                profesor.RegistroEmpleado = (int)reader[1];
                profesor.Nombre = (string)reader[2];
                profesor.ap_pat = (string)reader[3];
                profesor.ap_mat = (string)reader[4];
                profesor.Genero = (string)reader[5];
                profesor.Categoria = (string)reader[6];
                profesor.Correo = (string)reader[7];
                profesor.Celular = (string)reader[8];
                profesor.F_EdoCivil = (int)reader[9];
            }
            return profesor;
        }

        public bool EliminarProfesor(int IdProfesor)
        {
            string querySql = "DELETE FROM PROFESOR WHERE ID_Profe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",IdProfesor)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public List<Profesor> MostrarProfesores()
        {
            List<Profesor> list = null;
            string querySql = "SELECT * FROM PROFESOR";
            SqlParameter[] sqlParameters = null;
            SqlDataReader sqlDataReader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (sqlDataReader != null && sqlDataReader.HasRows)
            {
                list = new List<Profesor>();
                while (sqlDataReader.Read())
                {
                    list.Add(new Profesor()
                    {
                        ID_Profe = sqlDataReader.GetInt32(0),
                        RegistroEmpleado = sqlDataReader.GetInt32(1),
                        Nombre = sqlDataReader.GetString(2),
                        ap_pat = sqlDataReader.GetString(3),
                        ap_mat = sqlDataReader.GetString(4),
                        Genero = sqlDataReader.GetString(5),
                        Categoria = sqlDataReader.GetString(6),
                        Correo = sqlDataReader.GetString(7),
                        Celular = sqlDataReader.GetString(8),
                        F_EdoCivil = sqlDataReader.GetInt32(9)
                    });
                }
            }
            return list;

        }


        /*Métodos para ProfeGRupo*/

        public bool AgregarProfeGrupo(ProfeGrupo profeGrupo)
        {
            string querySql = "INSERT INTO ProfeGRupo (F_Profe,F_GruCuat,Extra,Extra2) VALUES (@FProf,@FGrup,@Ex,Exx)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("FProf",profeGrupo.F_Profe),
                new SqlParameter("Ex",profeGrupo.Extra),
                new SqlParameter("Exx",profeGrupo.Extra_dos),
                new SqlParameter("FGrup",profeGrupo.F_GrupoCuatrimestre)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool ModificarProfeGrupo(ProfeGrupo profeGrupo)
        {
            string querySql = "UPDATE ProfeGRupo SET F_Profe=@FProf,F_GruCuat=@FGrup,Extra=@Ex,Extra2=@Exx WHERE ID_ProfeGru=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                 new SqlParameter("id",profeGrupo.Id_ProfeGrupo),
                new SqlParameter("FProf",profeGrupo.F_Profe),
                new SqlParameter("Ex",profeGrupo.Extra),
                new SqlParameter("Exx",profeGrupo.Extra_dos),
                new SqlParameter("FGrup",profeGrupo.F_GrupoCuatrimestre)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool EliminarProfeGrupo(int idProfeGrupo)
        {
            string querySql = "DELETE FROM ProfeGRupo WHERE ID_ProfeGru=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idProfeGrupo)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public List<ProfeGrupo> BuscarGruposdeProfesor(int IdProfesor)
        {
            List<ProfeGrupo> list = null;
            string querySql = "SELECT * FROM ProfeGrupo WHERE F_Profe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",IdProfesor)
            };
            SqlDataReader reader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
            if (reader != null && reader.HasRows)
            {
                list = new List<ProfeGrupo>();
                while (reader.Read())
                {
                    list.Add(new ProfeGrupo()
                    {
                        Id_ProfeGrupo = reader.GetInt32(0),
                        F_Profe = reader.GetInt32(1),
                        F_GrupoCuatrimestre = reader.GetInt32(2),
                        Extra = reader.GetString(3),
                        Extra_dos = reader.GetString(4)
                    });
                }
            }
            return list;
        }
        //Mostrar con Joins



        /*Métodos para PositivoProfe*/

        public bool AgregarCasoPositivo(PositivoProfe positivoProfe)
        {
            string querySql = "INSERT INTO PositivoProfe (FechaConfirmado,Comprobacion,Antecedentes,NumContagio,Extra,F_Profe,Reisgo) " +
                "VALUES (@Fech,@Comp,@Ante,@NumC,@Ext,@FProf,@Riesgo)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("Fech",positivoProfe.FechaConfirmado),
                new SqlParameter("Comp",positivoProfe.Comprobacion),
                new SqlParameter("Ante",positivoProfe.Antecedentes),
                new SqlParameter("NumC",positivoProfe.NumContagio),
                new SqlParameter("Ext",positivoProfe.Extra),
                new SqlParameter("FProf",positivoProfe.F_Profe),
                new SqlParameter("Riesgo",positivoProfe.Riesgo)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool ModificarCasoPositivo(PositivoProfe positivo)
        {
            string querySql = "UPDATE PositivoProfe SET FechaConfirmado=@Fech,Comprobacion=@Comp,Antecedentes=@Ante," +
                "NumContagio=@NumC,Extra=@Ext,F_Profe=@FProf,Reisgo=@Riesgo WHERE Id_posProfe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",positivo.Id_posProfe),
                new SqlParameter("Fech",positivo.FechaConfirmado),
                new SqlParameter("Comp",positivo.Comprobacion),
                new SqlParameter("Ante",positivo.Antecedentes),
                new SqlParameter("NumC",positivo.NumContagio),
                new SqlParameter("Ext",positivo.Extra),
                new SqlParameter("FProf",positivo.F_Profe),
                new SqlParameter("Riesgo",positivo.Riesgo)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool EliminarCasoPositivo(int idPositivo) // Tal vez falle este método
        {
            string querySql = "DELETE FROM SeguimientoPRO WHERE F_positivoProfe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idPositivo)
            };
            this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
            querySql = "DELETE FROM Incapacidad WHERE id_posProfe=@id";

            this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
            querySql = "DELETE FROM PositivoProfe WHERE ID_ProfeGru=@id";

            bool result = this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);

            this.AccesoDatosSql.CerrarConexion();

            return result;


        }

        //public List<PositivoProfe> BuscarCasosPositivoDeProfesor(int idProfe)
        //{
        //    Profesor profesor = null;
        //    string querySql = "SELECT * FROM PositivoProfe WHERE RegistroEmpleado=@id";
        //    SqlParameter[] sqlParameters = new SqlParameter[]
        //    {
        //        new SqlParameter("id",IdProfesor)
        //    };
        //    SqlDataReader reader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);
        //    if (reader != null && reader.HasRows)
        //    {
        //        profesor = new Profesor();
        //        profesor.ID_Profe = (int)reader[0];
        //        profesor.RegistroEmpleado = (int)reader[1];
        //        profesor.Nombre = (string)reader[2];
        //        profesor.ap_pat = (string)reader[3];
        //        profesor.ap_mat = (string)reader[4];
        //        profesor.Genero = (string)reader[5];
        //        profesor.Categoria = (string)reader[6];
        //        profesor.Correo = (string)reader[7];
        //        profesor.Celular = (string)reader[8];
        //        profesor.F_EdoCivil = (int)reader[9];
        //    }
        //    return profesor;
        //}



        /* Métodos para SeguimientoProfesor */

        public bool AgregarSeguimientoCaso(SeguimientoProfesor seguimientoProfesor)
        {
            string querySql = "INSERT INTO SeguimientoPRO (F_positivoProfe,F_medico,Fecha,Form_Comunica,Reporte,Entrevista,Extra) " +
                "VALUES (@PosProf,@Med,@Fech,@FormC,@Report,@Entre,@Ex)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("PosProf",seguimientoProfesor.F_positivoProfe),
                new SqlParameter("Med",seguimientoProfesor.F_medico),
                new SqlParameter("Fech",seguimientoProfesor.Fecha),
                new SqlParameter("FormC",seguimientoProfesor.Form_Comunica),
                new SqlParameter("Report",seguimientoProfesor.Reporte),
                new SqlParameter("Entre",seguimientoProfesor.Entrevista),
                new SqlParameter("Ex",seguimientoProfesor.Extra)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        public bool ModificarSeguimientoCaso(SeguimientoProfesor seguimientoProfesor)
        {
            string querySql = "UPDATE SeguimientoPRO SET F_positivoProfe=@PosProf,F_medico=@Med,Fecha=@Fech," +
                "Form_Comunica=@FormC,Reporte=@Report,Entrevista=@Entre,Extra=@Ex WHERE id_Segui=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",seguimientoProfesor.id_Segui),
                new SqlParameter("PosProf",seguimientoProfesor.F_positivoProfe),
                new SqlParameter("Med",seguimientoProfesor.F_medico),
                new SqlParameter("Fech",seguimientoProfesor.Fecha),
                new SqlParameter("FormC",seguimientoProfesor.Form_Comunica),
                new SqlParameter("Report",seguimientoProfesor.Reporte),
                new SqlParameter("Entre",seguimientoProfesor.Entrevista),
                new SqlParameter("Ex",seguimientoProfesor.Extra)
            };
            return this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        }

        //public bool EliminarSeguimientoCaso(int idSeguimiento) // Es necesario pensar los controles para el método
        //{
        //    string querySql = "DELETE FROM SeguimientoPRO WHERE F_positivoProfe=@id";
        //    SqlParameter[] sqlParameters = new SqlParameter[]
        //    {
        //        new SqlParameter("id",idPositivo)
        //    };
        //    this.AccesoDatosSql.Modificar(querySql, sqlParameters, ref querySql);
        //}

        public List<SeguimientoProfesor> MostrarSeguimientoDeCaso(int idCasoPositivo)
        {
            List<SeguimientoProfesor> list = null;
            string querySql = "SELECT * FROM SeguimientoPRO WHERE F_positivoProfe=@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("id",idCasoPositivo)
            };
            SqlDataReader sqlDataReader = this.AccesoDatosSql.ConsultarReader(querySql, sqlParameters, ref querySql);

            if (sqlDataReader != null && sqlDataReader.HasRows)
            {
                list = new List<SeguimientoProfesor>();
                while (sqlDataReader.Read())
                {
                    list.Add(new SeguimientoProfesor()
                    {
                        id_Segui = sqlDataReader.GetInt32(0),
                        F_positivoProfe = sqlDataReader.GetInt32(1),
                        F_medico = sqlDataReader.GetInt32(2),
                        Fecha = sqlDataReader.GetDateTime(3),
                        Form_Comunica = sqlDataReader.GetString(4),
                        Reporte = sqlDataReader.GetString(5),
                        Entrevista = sqlDataReader.GetString(6),
                        Extra = sqlDataReader.GetString(7)
                    });
                }
            }
            return list;

        }
    }
}
