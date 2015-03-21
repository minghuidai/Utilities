using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMTech.Utilities
{

    public static class DBAccessUtil
    {
        /// <summary>
        /// Build database connection string
        /// </summary>
        /// <param name="server"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        static public string BuildConnectionString(string server, string database, int timeout = 30)
        {
            //const string CONNECTION_STRING = "server={0};database={1};User ID={2};Password={3};Connection Reset=FALSE providerName=System.Data.SqlClient";
            const string CONNECTION_STRING_1 = "Initial Catalog={1};Data Source={0}; Integrated Security=SSPI; Max Pool Size = 75000; Connection Timeout={2}";
            //const string CONNECTION_STRING_2 = "Initial Catalog={1};Data Source={0}; User ID={2}; password={3};Max Pool Size = 75000"

            return string.Format(CONNECTION_STRING_1, server, database, timeout);
        }




        /// <summary>
        /// Get the connection instance from connection string.
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static SqlConnection GetSqlConnectionInstance(string connStr)
        {
            try
            {
                System.Data.SqlClient.SqlConnection objConn = new System.Data.SqlClient.SqlConnection(connStr);
                objConn.Open();
                return objConn;
            }
            catch (Exception ex)
            {
                throw new Exception(Msg.FailedConnectionToSql + "/n ConnectionString: " + connStr, ex);
            }
        }




        /// <summary>
        /// Build a connection instance to olddb
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static OleDbConnection GetOledbConnectionInstance(string connStr)
        {
            try
            {
                OleDbConnection objConn = new OleDbConnection(connStr);
                objConn.Open();
                return objConn;
            }
            catch (Exception ex)
            {
                throw new Exception(Msg.FailedConnectionToOledb + ex.Message + "/n ConnectionString: " + connStr, ex);
            }
        }





        /// <summary>
        /// Execute general query and return a datatable.
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="objConn"></param>
        /// <returns></returns>
        public static DataTable QuerySql(string sSql, string connString, int timeout = 0)
        {
            using (SqlConnection sqlConn = DBAccessUtil.GetSqlConnectionInstance(connString))
            {
                SqlCommand objCmd = new SqlCommand(sSql, sqlConn);
                objCmd.CommandType = CommandType.Text;

                // Set timeout value for the query
                if (timeout > 0) objCmd.CommandTimeout = timeout;

                SqlDataAdapter objDA = new SqlDataAdapter(objCmd);

                DataTable objTable = new DataTable();
                try
                {
                    objDA.Fill(objTable);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //objCmd.Dispose()
                }

                return objTable;
            }
        }



        /// <summary>
        /// Execute general query and return a datatable.
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static DataTable QuerySql(string sSql, SqlConnection conn)
        {
            SqlCommand objCmd = new SqlCommand(sSql, conn);
            objCmd.CommandType = CommandType.Text;
            SqlDataAdapter objDA = new SqlDataAdapter(objCmd);

            DataTable objTable = new DataTable();
            try
            {
                objDA.Fill(objTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCmd.Dispose();
            }

            return objTable;
        }





        /// <summary>
        /// Quert for olddb database
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static DataTable QueryOledb(string sql, OleDbConnection conn)
        {
            OleDbCommand objCmd = new OleDbCommand(sql, conn);
            objCmd.CommandType = CommandType.Text;

            OleDbDataAdapter objDA = new OleDbDataAdapter(objCmd);

            DataTable objTable = new DataTable();
            try
            {
                objDA.Fill(objTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //objCmd.Dispose()
            }

            return objTable;
        }




        /// <summary>
        /// Execute a sql statement
        /// </summary>
        /// <param name="sql"></param>
        public static int ExecuteSql(string sql, string connString, int timeout = 30)
        {
            SqlConnection conn = DBAccessUtil.GetSqlConnectionInstance(connString);
            SqlCommand cmd = conn.CreateCommand();

            if (timeout > 0) cmd.CommandTimeout = timeout;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }




    }
}
