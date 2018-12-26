using System;
using System.Configuration;
using System.Data;
using Loja.Objeto;
using System.Text;

namespace Loja.Persistencia
{
    public class DistribuidoresOP : Persistencia
    {
        public string getSqlAdmin()
        {
            return String.Format(@"select id, nome, site, eMail, telefone, observacao 
                                   from distribuidores");
        }
        public void InsertDistribuidor(DistribuidorOT distribuidor)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendFormat(@"insert into distribuidores 
                                (nome, site, eMail, telefone, observacao) 
                                values 
                                ('{0}', '{1}', '{2}', '{3}', '{4}')",
                                    distribuidor.Nome,
                                    distribuidor.Site,
                                    distribuidor.EMail,
                                    distribuidor.Telefone,
                                    distribuidor.Observacao);
                ExecutarNonQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, sql));
            }
        }
        public void UpdateDistribuidor(DistribuidorOT distribuidor)
        {
            string sql = "";
            try
            {
                sql = String.Format(@"update distribuidores set 
                                         nome = '{0}', 
                                         site = '{1}', 
                                         eMail = '{2}',
                                         telefone = '{3}',
                                         observacao = '{4}'
                                         where id = {5}",
                                             distribuidor.Nome,
                                             distribuidor.Site,
                                             distribuidor.EMail,
                                             distribuidor.Telefone,
                                             distribuidor.Observacao,
                                             distribuidor.ID);
                ExecutarNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, sql));
            }
        }

        public DistribuidoresOT SelectDistribuidores()
        {
            DistribuidoresOT distribuidores = null;
            string sql = "";
            try
            {
                sql = String.Format(@"select id, nome, site, eMail, telefone, observacao 
                                         from distribuidores 
                                         order by nome");
                reader = ExecutarReader(sql.ToString());

                distribuidores = new DistribuidoresOT();
                while (reader.Read())
                {
                    DistribuidorOT _distribuidor = new DistribuidorOT();
                    _distribuidor = new DistribuidorOT();
                    _distribuidor.ID = Convert.ToInt32(reader["id"]);
                    _distribuidor.Nome = Convert.ToString(reader["nome"]);
                    _distribuidor.Site = Convert.ToString(reader["site"]);
                    _distribuidor.EMail = Convert.ToString(reader["eMail"]);
                    _distribuidor.Telefone = Convert.ToString(reader["telefone"]);
                    _distribuidor.Observacao = Convert.ToString(reader["observacao"]);
                    distribuidores.Add(_distribuidor);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, sql));
            }
            finally
            {
                fecharConexao();
            }
            return distribuidores;
        }
        public void SelectDistribuidor(ref DistribuidorOT distribuidor)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendFormat(@"select id, nome, site, eMail, telefone, observacao 
                                from distribuidores 
                                where id = {0} 
                                order by nome",
                                distribuidor.ID);

                reader = ExecutarReader(sql.ToString());
                if (reader.Read())
                {
                    distribuidor = new DistribuidorOT();
                    distribuidor.ID = Convert.ToInt32(reader["id"]);
                    distribuidor.Nome = Convert.ToString(reader["nome"]);
                    distribuidor.Site = Convert.ToString(reader["site"]);
                    distribuidor.EMail = Convert.ToString(reader["eMail"]);
                    distribuidor.Telefone = Convert.ToString(reader["telefone"]);
                    distribuidor.Observacao = Convert.ToString(reader["observacao"]);
                }
                else
                    distribuidor = null;

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