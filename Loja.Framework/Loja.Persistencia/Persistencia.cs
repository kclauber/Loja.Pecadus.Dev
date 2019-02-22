using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Text;

namespace Loja.Persistencia
{
    public abstract class Persistencia
    {
        public OdbcConnection con = new OdbcConnection();
        public bool isSandbox = (ConfigurationManager.AppSettings["isSandbox"].ToLower().Equals("true") ? true : false);

        protected void abrirConexao()
        {
            if (con.State != ConnectionState.Open)
            {
                string conS = "";
                
                if (!isSandbox)
                    conS = ConfigurationManager.ConnectionStrings["conPadrao"].ConnectionString;
                else
                    conS = ConfigurationManager.ConnectionStrings["conLocal"].ConnectionString;

                con.ConnectionString = conS;
                con.Open();
            }
        }
        protected void fecharConexao()
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
            if (reader != null && !reader.IsClosed)
            {
                reader.Close();
            }
        }

        protected DataSet ds;
        protected OdbcDataAdapter adapter;
        protected OdbcDataReader reader;

        protected OdbcDataAdapter GetAdapter(string sql)
        {
            abrirConexao(); 
            OdbcDataAdapter adap = new OdbcDataAdapter(sql, con);
            return adap;
        }
        private OdbcCommand GetCommand(string sql)
        {
            OdbcCommand cmd = new OdbcCommand();
            abrirConexao();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandTimeout = 300;
            return cmd;
        }
        protected void ExecutarNonQuery(string sql)
        {
            abrirConexao();
            (GetCommand(sql)).ExecuteNonQuery();
            fecharConexao();
        }
        protected OdbcDataReader ExecutarReader(string sql)
        {
            abrirConexao();
            OdbcDataReader reader = (GetCommand(sql)).ExecuteReader();
            return reader;
        }
    }
}