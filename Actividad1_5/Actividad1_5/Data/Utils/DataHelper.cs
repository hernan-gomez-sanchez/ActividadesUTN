using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Actividad1_5.Data.Utils
{
    internal class DataHelper
    {
        private static DataHelper _instancia;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.Conexion);
        }
        public static DataHelper GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DataHelper();
            }
            return _instancia;
        }
        public DataTable ExecuteSPQuery(string SP, List<ParametroSQL>? parametros)
        {
            DataTable tabla = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(SP, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                    }
                }
                tabla.Load(cmd.ExecuteReader());
                _connection.Close();
            }
            catch (SqlException)
            {
                tabla = null;
            }
            return tabla;
        }
        public int ExecuteSPDML(string SP, List<ParametroSQL>? parametros)
        {
            int rows;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(SP, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                    }
                }
                rows = cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }
            return rows;
        }
        public int ExecuteSPDMLTransact(string SP, SqlConnection connection, SqlTransaction transaction, List<ParametroSQL>? parametros,out List<ParametroSQL> Output)
        {
            int rows;
            List<SqlParameter> OutputParam = new List<SqlParameter>();
            Output = null;

            try
            {
                                
                var cmd = new SqlCommand(SP, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = transaction;
                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        if (parametro.Valor != null)
                        {
                            cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                        }
                        else
                        {
                            SqlParameter param = new SqlParameter(parametro.Nombre, parametro.Tipo);
                            param.Direction = ParameterDirection.Output;
                            OutputParam.Add(param);
                            cmd.Parameters.Add(param);
                            
                        }
                    }
                }
                
                rows = cmd.ExecuteNonQuery();

                if (OutputParam.Count != 0)
                {
                    Output = new List<ParametroSQL>();
                    foreach (var param in OutputParam)
                    {
                        Output.Add(new ParametroSQL(param.ParameterName,param.Value));
                    }
                }
               
            }
            catch (SqlException error)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                rows = 0;
               
            }
            return rows;
        }

        public int ExecuteSPDMLTransact(string SP, SqlConnection connection, SqlTransaction transaction, List<ParametroSQL>? parametros)
        {
            int rows;
           
            try
            {

                var cmd = new SqlCommand(SP, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = transaction;
                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    { 
                        cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                    }
                }

                rows = cmd.ExecuteNonQuery();
                
            }
            catch (SqlException)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                rows = 0;
            }
            return rows;
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }
    }
}
