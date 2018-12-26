using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using Loja.Objeto;

namespace Loja.Persistencia
{
    public class CategoriasOP : Persistencia
    {
        public string getSqlAdmin()
        {
            return String.Format(@"select id, idCategoriaPai, titulo, palavrasChave, ativo
                                    from categorias");
        }

        public void SelectCategoriasMenu(int categoriaPaiSelecionada, ref CategoriasOT categorias)
        {
            //Este select garante que a categoria Pai tenha filhos e que os filhos tenham produtos
            string sql = "";
            try
            {
                if (categoriaPaiSelecionada == -1)
                    sql = String.Format(@"select id, idCategoriaPai, titulo 
                                        from categorias c
                                      where ativo = 1 and idCategoriaPai IS NULL 
                                      and (select count(1) from categorias f where f.idCategoriaPai = c.id and 
                                          (select count(1) from produtos p where p.idcategoria = f.id)>0)
                                      order by titulo");
                else
                    sql = String.Format(@"select id, idCategoriaPai, titulo, 
                                        case when id = {0} then 1 else 0 end as categSelecionada
                                        from categorias c
                                      where ativo = 1 and idCategoriaPai IS NULL 
                                      and (select count(1) from categorias f where f.idCategoriaPai = c.id and 
                                          (select count(1) from produtos p where p.idcategoria = f.id)>0)
                                      order by categSelecionada desc, titulo",
                                          categoriaPaiSelecionada);
                reader = ExecutarReader(sql);
                CategoriaOT _categoria = null;
                while (reader.Read())
                {
                    _categoria = new CategoriaOT();
                    _categoria.ID = Convert.ToInt32(reader["id"]);
                    _categoria.IDCategoriaPai = -1;
                    _categoria.Titulo = Convert.ToString(reader["titulo"]);
                    categorias.Add(_categoria);

                    //Procurando por filhos
                    OdbcDataReader readerAux; //Novo reader para os "FILHOS"
                    //Este select garante que as categorias Filho tenham produtos
                    sql = String.Format(@"select id, idCategoriaPai, titulo 
                                            from categorias c
                                          where idCategoriaPai = {0} and ativo = 1 
                                          and (select count(1) from produtos p where p.idcategoria = c.id) > 0
                                          order by titulo",
                                          _categoria.ID);
                    readerAux = ExecutarReader(sql);
                    while (readerAux.Read())
                    {
                        _categoria = new CategoriaOT();
                        _categoria.ID = Convert.ToInt32(readerAux["id"]);
                        _categoria.IDCategoriaPai = Convert.ToInt32(readerAux["idCategoriaPai"]); ;
                        _categoria.Titulo = Convert.ToString(readerAux["titulo"]);
                        categorias.Add(_categoria);
                    }
                    readerAux.Close();
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
        }

        public CategoriasOT SelectCategoriasPai()
        {
            CategoriasOT categorias = null;
            string sql = "";
            try
            {
                sql = String.Format(@"select id, idCategoriaPai, titulo, palavrasChave, dtCadastro, ativo 
                                            from categorias 
                                         where idCategoriaPai IS NULL 
                                         order by titulo");
                reader = ExecutarReader(sql.ToString());

                if (reader.Read())
                {
                    categorias = new CategoriasOT();
                    while (reader.Read())
                    {
                        CategoriaOT _categoria = new CategoriaOT();
                        _categoria = new CategoriaOT();
                        _categoria.ID = Convert.ToInt32(reader["ID"]);
                        if (!String.IsNullOrEmpty(reader["idCategoriaPai"].ToString()))
                            _categoria.IDCategoriaPai = Convert.ToInt32(reader["idCategoriaPai"]);
                        _categoria.Titulo = Convert.ToString(reader["titulo"]);
                        _categoria.PalavrasChave = Convert.ToString(reader["palavrasChave"]);
                        _categoria.Ativo = Convert.ToBoolean(reader["ativo"]);
                        categorias.Add(_categoria);
                    }
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

            return categorias;
        }
        public CategoriasOT SelectCategoriasPai(int idCategoriaSelecionada)
        {
            CategoriasOT categorias = null;
            string sql = "";
            try
            {
                sql = String.Format(@"select id, idCategoriaPai, titulo, palavrasChave, dtCadastro, ativo 
                                            from categorias 
                                         where idCategoriaPai IS NULL and id != {0} 
                                         order by titulo",
                                         idCategoriaSelecionada);
                reader = ExecutarReader(sql.ToString());

                categorias = new CategoriasOT();
                while (reader.Read())
                {
                    CategoriaOT _categoria = new CategoriaOT();
                    _categoria = new CategoriaOT();
                    _categoria.ID = Convert.ToInt32(reader["ID"]);
                    if (!String.IsNullOrEmpty(reader["idCategoriaPai"].ToString()))
                        _categoria.IDCategoriaPai = Convert.ToInt32(reader["idCategoriaPai"]);
                    _categoria.Titulo = Convert.ToString(reader["titulo"]);
                    _categoria.PalavrasChave = Convert.ToString(reader["palavrasChave"]);
                    _categoria.Ativo = Convert.ToBoolean(reader["ativo"]);
                    categorias.Add(_categoria);
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
            return categorias;
        }
        public void SelectCategoria(ref CategoriaOT categoria)
        {
            string sql = "";
            try
            {
                sql = String.Format(@"select id, idCategoriaPai, titulo, palavrasChave, dtCadastro, ativo 
                                            from categorias 
                                         where id = {0}",
                                             categoria.ID);
                reader = ExecutarReader(sql.ToString());
                if (reader.Read())
                {
                    categoria = new CategoriaOT();
                    categoria.ID = Convert.ToInt32(reader["ID"]);
                    if (!String.IsNullOrEmpty(reader["idCategoriaPai"].ToString()))
                        categoria.IDCategoriaPai = Convert.ToInt32(reader["idCategoriaPai"]);
                    categoria.Titulo = Convert.ToString(reader["titulo"]);
                    categoria.PalavrasChave = Convert.ToString(reader["palavrasChave"]);
                    categoria.Ativo = Convert.ToBoolean(reader["ativo"]);
                }
                else
                    categoria = null;
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
        public CategoriasOT SelectCategorias(int idCategoriaPai)
        {
            CategoriasOT categorias = null;
            string sql = "";
            try
            {
                sql = String.Format(@"select id, idCategoriaPai, titulo, palavrasChave, dtCadastro, ativo 
                                            from categorias
                                         where idCategoriaPai = {0} order by titulo",
                                         idCategoriaPai);
                reader = ExecutarReader(sql.ToString());

                categorias = new CategoriasOT();
                while (reader.Read())
                {
                    CategoriaOT _categoria = new CategoriaOT();
                    _categoria.ID = Convert.ToInt32(reader["ID"]);
                    if (!String.IsNullOrEmpty(reader["idCategoriaPai"].ToString()))
                        _categoria.IDCategoriaPai = Convert.ToInt32(reader["idCategoriaPai"]);
                    _categoria.Titulo = Convert.ToString(reader["titulo"]);
                    _categoria.PalavrasChave = Convert.ToString(reader["palavrasChave"]);
                    _categoria.Ativo = Convert.ToBoolean(reader["ativo"]);
                    categorias.Add(_categoria);
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

            return categorias;
        }

        public void InsertCategoria(CategoriaOT categoria)
        {
            string sql = "";
            try
            {
                sql = String.Format(@"insert into categorias 
                                         (idCategoriaPai, titulo, palavrasChave, dtCadastro, ativo) 
                                         values
                                         ({0}, '{1}', '{2}', now(), {3})",
                                             (categoria.IDCategoriaPai == -1 ? "null" : categoria.IDCategoriaPai.ToString()),
                                             categoria.Titulo,
                                             categoria.PalavrasChave,
                                             categoria.Ativo);
                ExecutarNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, sql));
            }
        }
        public void UpdateCategoria(CategoriaOT categoria)
        {
            string sql = "";
            try
            {
                sql = String.Format(@"update categorias 
                                         set idCategoriaPai = {0}, 
                                         titulo = '{1}', 
                                         palavrasChave = '{2}', 
                                         ativo = {3}
                                         where id = {4}",
                                             (categoria.IDCategoriaPai == -1 ? "null" : categoria.IDCategoriaPai.ToString()),
                                             categoria.Titulo,
                                             categoria.PalavrasChave,
                                             categoria.Ativo,
                                             categoria.ID);
                ExecutarNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, sql));
            }
        }
    }
}