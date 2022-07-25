using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaAccesoDatos
{
    public class AccesoDatos
    {
        private string cadConexion;
        private SqlConnection Connection;

        public AccesoDatos(string cadenaBD)
        {
            cadConexion = cadenaBD;
        }

        public bool AbrirConexion(ref string msg)
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                return true;
            else
            {
                Connection = new SqlConnection();
                Connection.ConnectionString = cadConexion;
                try
                {
                    Connection.Open();
                    return true;
                }
                catch (Exception r)
                {
                    msg = r.Message;
                    Connection = null; //Devuelve una conexion nula
                }
                return false;
            }

        }

        public void CerrarConexion()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }

        public bool Modificar(string querySql, SqlParameter[] sqlParameters, ref string mensaje)
        {
            bool result;
            if (this.AbrirConexion(ref mensaje))
            {
                SqlCommand carrito = new SqlCommand();
                carrito.CommandText = querySql;
                carrito.Connection = this.Connection;
                if (sqlParameters.Length > 0)
                {
                    foreach (var Param in sqlParameters)
                    {
                        carrito.Parameters.Add(Param);
                    }
                }

                try
                {
                    _ = carrito.ExecuteNonQuery() > 0 ? result = true : result = false;
                    mensaje = "Inserción realizada";
                    CerrarConexion();
                    return result;

                }
                catch (Exception s)
                {
                    CerrarConexion();
                    mensaje = s.Message;
                    throw;
                }

            }
            return false;
        }

        public bool ModificarSinCerrar(string querySql, SqlParameter[] sqlParameters, ref string mensaje)
        {
            bool result;
            if (this.AbrirConexion(ref mensaje))
            {
                SqlCommand carrito = new SqlCommand();
                carrito.CommandText = querySql;
                carrito.Connection = this.Connection;
                if (sqlParameters.Length > 0)
                {
                    foreach (var Param in sqlParameters)
                    {
                        carrito.Parameters.Add(Param);
                    }
                }

                try
                {
                    _ = carrito.ExecuteNonQuery() > 0 ? result = true : result = false;
                    return result;

                }
                catch (Exception s)
                {
                    mensaje = s.Message;
                    throw;
                }

            }
            return false;
        }

        public DataSet ConsultaDS(string querySql, SqlParameter[] sqlParameters, ref string mensaje)
        {
            if (this.AbrirConexion(ref mensaje))
            {
                DataSet DS_salida = new DataSet();
                SqlCommand carrito = new SqlCommand();
                carrito.CommandText = querySql;
                carrito.Connection = this.Connection;

                SqlDataAdapter trailer = new SqlDataAdapter();
                trailer.SelectCommand = carrito;

                if (sqlParameters != null && sqlParameters.Length > 0)
                {
                    foreach (SqlParameter param in sqlParameters)
                    {
                        carrito.Parameters.Add(param);
                    }
                }

                try
                {
                    trailer.Fill(DS_salida, "Consulta1");
                    mensaje = "Consulta Correcta en DataSet";
                    this.CerrarConexion();
                    return DS_salida;
                }
                catch (Exception a)
                {
                    mensaje = "error: " + a.Message;
                }
            }
            return null;
        }

        public SqlDataReader ConsultarReader(string querySql, SqlParameter[] parameters, ref string mensaje)
        {
            if (this.AbrirConexion(ref mensaje))
            {
                mensaje = "No hay conexion a la BD";
                SqlCommand carrito = new SqlCommand();
                carrito.CommandText = querySql;
                carrito.Connection = this.Connection;
                if (parameters != null && parameters.Length > 0)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        carrito.Parameters.Add(param);
                    }
                }
                try
                {
                    mensaje = "Consulta Correcta DataReader";
                    return carrito.ExecuteReader();

                }
                catch (Exception a)
                {
                    mensaje = "error: " + a.Message;
                }

            }
            return null;
        }

        public bool MultiplesConsultasDataSet(string querySql, ref string mensaje, SqlParameter[] sqlParameters, ref DataSet dataset1, string nomConsulta)
        {
            if (this.AbrirConexion(ref mensaje))
            {
                SqlCommand carrito = new SqlCommand();
                SqlDataAdapter trailer = new SqlDataAdapter();

                carrito.CommandText = querySql;
                carrito.Connection = this.Connection;
                trailer.SelectCommand = carrito;

                if (sqlParameters != null && sqlParameters.Length > 0)
                {
                    foreach (SqlParameter param in sqlParameters)
                    {
                        carrito.Parameters.Add(param);
                    }
                }

                try
                {
                    mensaje = "Consulta correcta en el DataSet";
                    if (dataset1.Tables.Contains(nomConsulta))
                    {
                        dataset1.Tables[nomConsulta].Clear();
                    }
                    trailer.Fill(dataset1, nomConsulta);


                    return true;
                }
                catch (Exception a)
                {
                    mensaje = "Error: " + a.Message;
                }

            }
            return false;
        }
    }
}
