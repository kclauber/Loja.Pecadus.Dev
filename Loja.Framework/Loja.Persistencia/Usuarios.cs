using System;
using System.Text;

namespace Loja.Persistencia
{
    public class UsuariosOP : Persistencia
    {
        public bool Login(string user, string pass)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendFormat("select id from usuarios where nome = '{0}' and senha = '{1}'",
                                  user, pass);
                reader = ExecutarReader(sql.ToString());
                return reader.Read();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, sql));
            }
            finally
            {
                fecharConexao();
            }
        }
    }
}