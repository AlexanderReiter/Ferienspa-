using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace Verwaltung_Themenfahrten
{
    public class DB
    {
        private OdbcConnection connection = null;
        private string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
            }
        }

        public OdbcConnection Connection
        {
            get
            {
                if (connection == null) { connection = new OdbcConnection(ConnectionString); }
                return connection;
            }
        }

        public OdbcCommand CreateCommand(string sql, params object[] parametervalues)
        {
            OdbcCommand cmd = new OdbcCommand(sql, Connection);
            foreach (object parametervalue in parametervalues)
            {
                cmd.Parameters.AddWithValue("", parametervalue);
            }
            return cmd;
        }

        public void Open()
        {
            Connection.Open();
        }

        public void Close()
        {
            Connection.Close();
        }

        public DataTable Query(string sql, params object[] parametervalues)
        {
            DataTable dt = new DataTable();
            new OdbcDataAdapter(CreateCommand(sql, parametervalues)).Fill(dt);
            return dt;
        }

        internal int ExecuteNonQuery(string sql, params object[] parametervalues)
        {
            OdbcCommand cmd = CreateCommand(sql, parametervalues);
            Open();
            int ret = cmd.ExecuteNonQuery();
            Close();
            return ret;
        }

        internal object ExecuteScalar(string sql, params object[] parametervalues)
        {
            OdbcCommand cmd = CreateCommand(sql, parametervalues);
            Open();
            object ret = cmd.ExecuteScalar();
            Close();
            return ret;
        }
    }
}