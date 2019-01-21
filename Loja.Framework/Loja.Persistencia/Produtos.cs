using System;
using System.Data;
using System.Text;
using Loja.Objeto;

namespace Loja.Persistencia
{
    public class ProdutosOP : Persistencia
    {
        private string getStringSql(string where, string order)
        {
            if (String.IsNullOrEmpty(where))
                where = " where 1=1";

            return String.Format(
                        @"SELECT 
                            a.id,
                            a.ean,
                            d.id as idDistribuidor, 
                            d.nome as nomeDistribuidor,
                            c.id as idCategoria, 
                            c.idCategoriaPai, 
                            c.titulo as tituloCategoria, 
                            (select titulo from categorias where id = c.idCategoriaPai) as tituloCategoriaPai,
                            c.palavrasChave as categPalavrasChave, 
                            a.titulo, 
                            a.descricaoCurta, 
                            a.descricaoCompleta, 
                            a.palavrasChave, 
                            a.observacao, 
                            a.preco, 
                            a.precoCusto,
                            a.markUp,
                            a.desconto, 
                            a.frete,
                            a.peso, 
                            a.dtCadastro, 
                            a.estoque, 
                            a.ativo,
                            a.exibirHome, 
                            a.destaque,
                            case 
                                when a.estoque <= 0 
                                then 0 
                                else 1 
                            end as hasProduto, 
                            case 
                                when (select x.id from produtosImagens x where x.idProduto = a.id limit 1) is null 
                                then 0 
                                else 1 
                            end as hasImg 
                        FROM (produtos a JOIN categorias c ON a.idCategoria = c.id 
                              join distribuidores d on a.idDistribuidor = d.id) 
                        {0}
                        GROUP BY 
                            a.id, c.id, c.idCategoriaPai, a.titulo, a.descricaoCurta, a.descricaoCompleta, 
                            a.palavrasChave, a.preco, a.frete, a.peso, a.dtCadastro, a.estoque, a.ativo 
                        {1}",
                        where, order);
        }
        public string getSqlAdmin()
        {
            return String.Format(@"SELECT id, ean, titulo, preco, estoque, ativo 
                                        FROM produtos 
                                   ORDER BY id");
        }
        public string getSqlAdmin(string busca, bool? temEstoque, bool? ativo)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"SELECT id, ean, titulo, preco, estoque, ativo 
                                    FROM produtos");

            if (!String.IsNullOrEmpty(busca))
                sql.AppendFormat(@" and (id = '{0}' or titulo like '%{0}%' or ean = '%{0}%')", busca);

            if (temEstoque != null)
            {
                if (temEstoque.Value)
                    sql.Append(" and estoque > 0");
                else
                    sql.Append(" and estoque <= 0");
            }

            if (ativo != null)
            {
                if (ativo.Value)
                    sql.Append(" and ativo = 1");
                else
                    sql.Append(" and ativo = 0");
            }

            sql.Append(" ORDER BY id");

            return sql.ToString();
        }
        private ProdutosOT SelectProdutos(int inicio, int pagina, string sql)
        {
            //try
            //{
                ProdutosOT produtos = new ProdutosOT();
                ds = new DataSet();

                adapter = GetAdapter(sql);
                adapter.Fill(ds, "Produtos");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (inicio > -1 && pagina > -1)
                        adapter.Fill(ds = new DataSet(), inicio, pagina, "Produtos");
                    else
                        adapter.Fill(ds = new DataSet(), "Produtos");

                    //Passando os dados do DataSet para o objeto
                    foreach (DataRow _row in ds.Tables["Produtos"].Rows)
                    {
                        ProdutoOT _produto = new ProdutoOT();
                        _produto.ID = Convert.ToInt32(_row["id"]);
                        _produto.EAN = Convert.ToString(_row["ean"]);
                        _produto.Titulo = Convert.ToString(_row["titulo"]);
                        _produto.DescricaoCurta = Convert.ToString(_row["descricaoCurta"]);
                        _produto.DescricaoCompleta = Convert.ToString(_row["descricaoCompleta"]);
                        _produto.PalavrasChave = Convert.ToString(_row["palavrasChave"]);
                        _produto.Observacao = Convert.ToString(_row["observacao"]);

                        _produto.Preco = Convert.ToDouble(_row["preco"]);
                        _produto.PrecoCusto = Convert.ToDouble(_row["precoCusto"]);
                        _produto.MarkUp = Convert.ToDouble(_row["markUp"]);
                        _produto.Desconto = Convert.ToInt32(_row["desconto"]);
                        _produto.Frete = Convert.ToDouble(_row["frete"]);
                        _produto.Peso = Convert.ToInt32(_row["peso"]);
                        _produto.Estoque = Convert.ToInt32(_row["estoque"]);

                        _produto.DtCadastro = Convert.ToDateTime(_row["dtCadastro"]);
                        _produto.ExibirHome = Convert.ToBoolean(_row["exibirHome"]);
                        _produto.Destaque = Convert.ToBoolean(_row["destaque"]);
                        _produto.Ativo = Convert.ToBoolean(_row["ativo"]);

                        //Dados do distribuidor
                        _produto.Distribuidor.ID = Convert.ToInt32(_row["idDistribuidor"]);
                        _produto.Distribuidor.Nome = Convert.ToString(_row["nomeDistribuidor"]);

                        //Dados da categoria
                        _produto.Categoria.ID = Convert.ToInt32(_row["idCategoria"]);
                        _produto.Categoria.IDCategoriaPai = Convert.ToInt32(_row["idCategoriaPai"]);
                        _produto.Categoria.Titulo = Convert.ToString(_row["tituloCategoria"]);
                        _produto.Categoria.TituloCategoriaPai = Convert.ToString(_row["tituloCategoriaPai"]);

                        _produto.Imagens = SelectImagensProduto(_produto.ID);
                        _produto.Videos = SelectVideoProduto(_produto.ID);

                        produtos.Add(_produto);
                    }
                }
                else
                    produtos = null;

                return produtos;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, sql));
            //}
            //finally
            //{
            //    fecharConexao();
            //}
        }

        public ProdutoOT SelectProduto(int id, int inicio, int pagina)
        {
            return ((ProdutosOT)SelectProdutos(inicio, pagina, getStringSql("where a.id = " + id, "")))[0];
        }
        public ProdutoOT SelectProduto(string ean, int inicio, int pagina)
        {
            return ((ProdutosOT)SelectProdutos(inicio, pagina, getStringSql("where a.ean = '" + ean + "'", "")))[0];
        }

        public ProdutosOT SelectProdutosAtivos(int inicio, int pagina)
        {
            return SelectProdutos(inicio, pagina,
                                  getStringSql("where a.ativo = 1", " order by hasProduto desc, hasImg desc, a.dtCadastro desc"));
        }
        public ProdutosOT SelectProdutosTodos()
        {
            return SelectProdutos(-1, -1, getStringSql("", " order by hasProduto desc, hasImg desc, a.dtCadastro desc"));
        }
        public ProdutosOT SelectProdutosCarrinho(ProdutosOT produtos)
        {
            foreach (ProdutoOT _produto in produtos)
            {
                ProdutoOT _aux = new ProdutoOT();
                _aux = SelectProduto(_produto.ID, -1, -1);

                _produto.EAN = _aux.EAN;
                _produto.Titulo = _aux.Titulo;
                _produto.DescricaoCurta = _aux.DescricaoCurta;
                _produto.DescricaoCompleta = _aux.DescricaoCompleta;
                _produto.PalavrasChave = _aux.PalavrasChave;
                _produto.Preco = _aux.Preco;
                _produto.PrecoCusto = _aux.PrecoCusto;
                _produto.Desconto = _aux.Desconto;
                _produto.Frete = _aux.Frete;
                _produto.Peso = _aux.Peso;
                _produto.DtCadastro = _aux.DtCadastro;
                _produto.Estoque = _aux.Estoque;
                _produto.ExibirHome = _aux.ExibirHome;
                _produto.Destaque = _aux.Destaque;
                _produto.Ativo = _aux.Ativo;

                //Dados do distribuidor
                _produto.Distribuidor.ID = _aux.Distribuidor.ID;
                _produto.Distribuidor.Nome = _aux.Distribuidor.Nome;

                //Dados da categoria
                _produto.Categoria.ID = _aux.Categoria.ID;
                _produto.Categoria.IDCategoriaPai = _aux.Categoria.IDCategoriaPai;
                _produto.Categoria.Titulo = _aux.Categoria.Titulo;
                _produto.Categoria.TituloCategoriaPai = _aux.Categoria.TituloCategoriaPai;

                _produto.Imagens = _aux.Imagens;
                _produto.Videos = _aux.Videos;
            }

            return produtos;
        }
        public ProdutosOT SelectProdutosCategoria(int idCategoria, int inicio, int pagina)
        {
            return SelectProdutos(inicio, pagina,
                                  getStringSql("where a.ativo = 1 and a.idCategoria = " + idCategoria, "order by hasProduto desc, hasImg desc"));
        }
        public ProdutosOT SelectProdutosCategoriaPai(int idCategoriaPai, int inicio, int pagina)
        {
            return SelectProdutos(inicio, pagina,
                                  getStringSql("where a.ativo = 1 and c.idCategoriaPai = " + idCategoriaPai, "order by hasProduto desc, hasImg desc"));
        }
        public ProdutosOT SelectProdutosRelacionados(int idProduto, int idCategoria, int inicio, int pagina)
        {
            return SelectProdutos(inicio, pagina,
                                  getStringSql("where a.ativo = 1 and estoque > 0 and a.idCategoria = " + idCategoria + " and a.id <> " + idProduto, "order by rand()"));
        }
        public ProdutosOT SelectProdutosBusca(string palavrasBusca, int inicio, int pagina)
        {
            try
            {
                string[] arrbusca = palavrasBusca.Replace('-', ' ').Split(' ');

                //buscando resultados aproximados
                string buscaAprox = "";
                for (int a = 0; a < arrbusca.Length; a++)
                {
                    buscaAprox += "";
                    if (buscaAprox != "")
                        buscaAprox += " or ";

                    buscaAprox += String.Format(
                                  @"({1} like lower('%{0}%') or 
                                     {2} like lower('%{0}%') or 
                                     {3} like lower('%{0}%') or 
                                     {4} like lower('%{0}%'))",
                                          arrbusca[a],
                                          strReplace("a.titulo"),
                                          strReplace("a.descricaocurta"),
                                          strReplace("a.descricaocompleta"),
                                          strReplace("a.palavraschave")
                                  );
                }

                ProdutosOT produtos = SelectProdutos(inicio, pagina,
                                                     getStringSql("where a.ativo = 1 and (" + buscaAprox + ") ", "order by a.titulo asc, hasproduto desc, hasimg desc"));
                GravaBusca(palavrasBusca);
                ////////////////////////////////////

                return produtos;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, string.Empty));
            }
            finally
            {
                fecharConexao();
            }
        }
        public void GravaBusca(string palavrasBusca)
        {
            try
            {
                string sql = String.Format("select id from busca where palavra_chave = '{0}'",
                                            palavrasBusca);
                reader = ExecutarReader(sql);
                if (reader.Read())
                {
                    sql = String.Format("update busca set hits = hits + 1 where id = {0}", reader["id"]);
                    ExecutarNonQuery(sql);
                }
                else
                {
                    sql = String.Format(@"insert into busca (palavra_chave, hits, dtAtualizacao) 
                                                 values ('{0}', 1, now())",
                                         palavrasBusca);
                    ExecutarNonQuery(sql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, string.Empty));
            }
        }

        private string strReplace(string campo)
        {
            return
            String.Format(
                @"replace(replace(replace(replace(replace(replace(replace(
                                replace(replace(replace(replace(replace(replace(
                                  lower({0})
                                , 'á', 'a'), 'à', 'a'), 'ã', 'a'), 'â', 'a')
                                , 'é', 'e'), 'ê', 'e'), 'í', 'i')
                                , 'ó', 'o'), 'ô', 'o'), 'õ', 'o')
                                , 'ú', 'u'), 'ü', 'u'), 'ç', 'c')", campo);
        }
        public ProdutosOT SelectProdutosDestaque()
        {
            return SelectProdutos(0, 10, getStringSql("where a.ativo = 1 and destaque = 1", "order by a.preco desc"));
        }
        public ProdutosOT SelectProdutosHome()
        {
            return SelectProdutos(0, 16, getStringSql("where a.ativo = 1 and exibirHome = 1", "order by a.preco desc"));
        }
        /// <summary>
        /// Utilizado no gráfico da tela de Admin
        /// </summary>
        /// <returns></returns>
        public DataTable RelatorioProdutos()
        {
            string sql = "";
            try
            {
                sql = "SELECT ";
                sql += " ifnull(sum(1),0) QtdProdutos, ";
                sql += " (select ifnull(sum(1),0) from produtos where estoque > 0) ComEstoque, ";
                sql += " (select ifnull(sum(1),0) from produtos where estoque <= 0) SemEstoque, ";
                sql += " (select ifnull(sum(1),0) from produtos where ativo = 1) Ativos, ";
                sql += " (select ifnull(sum(1),0) from produtos where ativo = 1 and estoque > 0) AtivosComEstoque, ";
                sql += " (select ifnull(sum(1),0) from produtos where ativo = 1 and estoque <= 0) AtivosSemEstoque, ";
                sql += " (select ifnull(sum(1),0) from produtos where ativo = 0) Inativos, ";
                sql += " (select ifnull(sum(1),0) from produtos where ativo = 0 and estoque > 0) InativosComEstoque, ";
                sql += " (select ifnull(sum(1),0) from produtos where ativo = 0 and estoque <= 0) InativosSemEstoque, ";
                sql += " (SELECT ifnull(sum(1),0) from produtos where id not in (SELECT idproduto FROM produtosImagens)) SemImagem, ";
                sql += " (select ifnull(sum(preco),0) from produtos) VlrTotal ";
                sql += " FROM produtos";
                ds = new DataSet();

                adapter = GetAdapter(sql);
                adapter.Fill(ds);

                return ds.Tables[0];
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

        public DataView[] SelectTagCloud()
        {
            try
            {
                DataView[] dv = new DataView[2];

                DataTable dt = new DataTable("Links");
                dt.Columns.Add("id");
                dt.Columns.Add("idCategoriaPai");
                dt.Columns.Add("titulo");
                dt.Columns.Add("tituloPai");
                dt.Columns.Add("hits");
                DataRow dr;

                DataTable dtPar = new DataTable("Parametros");
                dtPar.Columns.Add("maxValueBusca");
                DataRow drPar = dtPar.NewRow();
                drPar["maxValueBusca"] = 0;

                //Select das palavras buscadas
                string sql = String.Format(@"select id, palavra_chave, hits from busca
                                             order by hits desc 
                                             limit 25");

                reader = ExecutarReader(sql);
                while (reader.Read())
                {
                    dr = dt.NewRow();
                    dr["id"] = reader["id"];
                    dr["idCategoriaPai"] = "";
                    dr["titulo"] = reader["palavra_chave"];
                    dr["tituloPai"] = "";
                    dr["hits"] = reader["hits"];
                    dt.Rows.Add(dr);

                    if (Convert.ToInt32(drPar["maxValueBusca"]) < Convert.ToInt32(reader["hits"]))
                        drPar["maxValueBusca"] = Convert.ToInt32(reader["hits"]);
                }
                dtPar.Rows.Add(drPar);

                dv[0] = dt.DefaultView;
                dv[0].Sort = "titulo";
                dv[1] = dtPar.DefaultView;

                return dv;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, string.Empty));
            }
            finally
            {
                fecharConexao();
            }
        }

        public int InsertProdutos(ProdutoOT produto)
        {
            string sql = "";
            try
            {
                sql = String.Format(@"insert into produtos (
                                         ean, 
                                         idDistribuidor,
                                         idCategoria,
                                         titulo,
                                         descricaoCurta,
                                         descricaoCompleta,
                                         palavrasChave,
                                         preco,
                                         precoCusto,
                                         desconto,
                                         frete,
                                         peso,
                                         estoque,
                                         exibirHome,
                                         ativo,
                                         destaque,
                                         dtCadastro,
                                         observacao,
                                         markUp)
                                         values
                                         ('{0}',{1},{2},'{3}','{4}','{5}','{6}',{7},{8},{9},{10},{11},{12},{13},{14},{15},now(),'{16}',{17})",
                                             produto.EAN,
                                             produto.Distribuidor.ID,
                                             produto.Categoria.ID,
                                             produto.Titulo,
                                             produto.DescricaoCurta,
                                             produto.DescricaoCompleta,
                                             produto.PalavrasChave,
                                             produto.Preco.ToString().Replace(",", "."),
                                             produto.PrecoCusto.ToString().Replace(",", "."),
                                             produto.Desconto.ToString().Replace(",", "."),
                                             produto.Frete.ToString().Replace(",", "."),
                                             produto.Peso.ToString().Replace(",", "."),
                                             produto.Estoque,
                                             produto.ExibirHome,
                                             produto.Ativo,
                                             produto.Destaque,
                                             produto.Observacao,
                                             produto.MarkUp);
                ExecutarNonQuery(sql);

                sql = String.Format("select max(id) as idN from produtos");
                reader = ExecutarReader(sql);
                int retorno;
                if (reader.Read())
                    retorno = Convert.ToInt32(reader[0]);
                else
                    retorno = 0;

                return retorno;
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
        public void UpdateProdutos(ProdutoOT produto)
        {
            try
            {
                string sql = String.Format(@"update produtos set
                                         ean='{0}',
                                         idDistribuidor={1},
                                         idCategoria={2},
                                         titulo='{3}',
                                         descricaoCurta='{4}',
                                         descricaoCompleta='{5}',
                                         palavrasChave='{6}',
                                         preco={7},
                                         precoCusto={8},
                                         desconto={9},
                                         frete={10},
                                         peso={11},
                                         estoque={12},
                                         exibirHome={13},
                                         ativo={14},
                                         destaque={15},
                                         observacao='{16}',
                                         markUp={17}
                                         where id = {18}",
                                             produto.EAN,
                                             produto.Distribuidor.ID,
                                             produto.Categoria.ID,
                                             produto.Titulo,
                                             produto.DescricaoCurta,
                                             produto.DescricaoCompleta,
                                             produto.PalavrasChave,
                                             produto.Preco.ToString().Replace(",", "."),
                                             produto.PrecoCusto.ToString().Replace(",", "."),
                                             produto.Desconto.ToString().Replace(",", "."),
                                             produto.Frete.ToString().Replace(",", "."),
                                             produto.Peso.ToString().Replace(",", "."),
                                             produto.Estoque,
                                             produto.ExibirHome,
                                             produto.Ativo,
                                             produto.Destaque,
                                             produto.Observacao,
                                             produto.MarkUp,
                                             produto.ID);
                ExecutarNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, string.Empty));
            }
        }
        public void AtualizaEstoque(int ID, int quantidade)
        {
            try
            {
                string sql = String.Format(@"update produtos set 
                                                    estoque = estoque + {0}
                                                    where id = {1}",
                                                    quantidade, 
                                                    ID);
                ExecutarNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, string.Empty));
            }
        }

        #region -- Imagens e vídeos --
        public ProdutoVideosOT SelectVideoProduto(int idProduto)
        {
            string sql = "";
            try
            {
                sql = "select id, idProduto, titulo, ativo from produtosVideos where idProduto = " + idProduto;
                ds = new DataSet();

                adapter = GetAdapter(sql);
                adapter.Fill(ds, "produtoVideos");

                ProdutoVideosOT videos = new ProdutoVideosOT();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //Passando os dados do DataSet para o objeto
                    foreach (DataRow _row in ds.Tables["produtoVideos"].Rows)
                    {
                        ProdutoVideoOT _video = new ProdutoVideoOT();
                        _video.ID = Convert.ToInt32(_row["id"]);
                        _video.Destaque = Convert.ToBoolean(_row["destaque"]);
                        _video.Titulo = Convert.ToString(_row["titulo"]);
                        _video.DtCadastro = Convert.ToDateTime(_row["dtCadastro"]);
                        _video.Ativo = Convert.ToBoolean(_row["ativo"]);
                        videos.Add(_video);
                    }
                }

                return videos;
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
        public ProdutoImagensOT SelectImagensProduto(int idProduto)
        {
            string sql = "";
            try
            {
                sql = "select id, idProduto, destaque, titulo, dtCadastro, ativo from produtosImagens where idProduto = " + idProduto;
                ds = new DataSet();

                adapter = GetAdapter(sql);
                adapter.Fill(ds, "produtoImagens");

                ProdutoImagensOT imagens = new ProdutoImagensOT();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //Passando os dados do DataSet para o objeto
                    foreach (DataRow _row in ds.Tables["produtoImagens"].Rows)
                    {
                        ProdutoImagemOT _imagem = new ProdutoImagemOT();
                        _imagem.ID = Convert.ToInt32(_row["id"]);
                        _imagem.Destaque = Convert.ToBoolean(_row["destaque"]);
                        _imagem.Titulo = Convert.ToString(_row["titulo"]);
                        _imagem.DtCadastro = Convert.ToDateTime(_row["dtCadastro"]);
                        _imagem.Ativo = Convert.ToBoolean(_row["ativo"]);
                        imagens.Add(_imagem);
                    }
                }

                return imagens;
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

        public void InsertVideos(int idProd, string arquivo)
        {
            string sql = "insert into produtosVideos " +
                         " (idProduto, titulo, dtCadastro, ativo)" +
                         " values (" + idProd + ", '" + arquivo + "', now(), 1)";
            ExecutarNonQuery(sql);
        }
        public void InsertImagens(int idProd, string arquivo)
        {
            string sql;
            sql = "insert into produtosImagens " +
                  " (idProduto, titulo, dtCadastro, ativo)" +
                  " values (" + idProd + ", '" + arquivo + "', now(), 1)";
            ExecutarNonQuery(sql);
        }

        public void DeleteImagem(int id)
        {
            string sql = "delete from produtosImagens where id = " + id;
            ExecutarNonQuery(sql);
        }
        public void DeleteVideo(int id)
        {
            string sql = "delete from produtosVideos where id = " + id;
            ExecutarNonQuery(sql);
        }
        #endregion

        public static ProdutoOT CarregaProdutoFalso(int id = 1)
        {
            ProdutoOT _produto = new ProdutoOT();
            _produto.ID = id;
            //_produto.EAN = Convert.ToString(_row["ean"]);
            _produto.Titulo = "Titulo Titulo Titulo Titulo Titulo";
            _produto.DescricaoCurta = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam metus massa, facilisis vel volutpat ut, tempor et nisi.</br>
                                        Lorem ipsum dolor sit amet, consectetur adipiscing elit.Etiam metus massa, facilisis vel volutpat ut, tempor et nisi.";
            _produto.DescricaoCompleta = @"<p>Coelho de pernas para o AR, com compartimento secreto, acompanha um vibrador personal 13 cm na cor rosa e cadeado de segurança.</p>
                                           <p>Este COELHO em pelúcia antialérgica, traz dentro do seu compartimento secreto uma outra surpresa: um vibrador personal rosa de 13cm. Tudo fechado com cadeado de segurança.</p>
                                           <p>Os brinquedos em pelúcia são ideais para presentes e decoração sensual . Possui um compartimento interno para guardar os acessórios eróticos de forma discreta e segura.</p>
                                           <ul>
                                             <li><strong>Tamanho:</strong> Aproximadamente 45cm</li>
                                             <li><strong>Cor:</strong> Branco e Rosa</li>
                                             <li><strong>Material:</strong> Pelúcia anti-alérgica </li>
                                             <li><strong>Embalagem:</strong> Saco plástico com solapa</li>
                                             <li><strong>Acompanha:</strong> 1 Cadeado com chave e 1 vibrador personal</li>
                                           </ul>";
            _produto.PalavrasChave = @"Coelho de Pelúcia, Coelho de Pelúcia, Coelho de Pelúcia, Coelho de Pelúcia, Coelho de Pelúcia";
            //_produto.Observacao = Convert.ToString(_row["observacao"]);

            _produto.Preco = 200.89;
            //_produto.PrecoCusto = Convert.ToDouble(_row["precoCusto"]);
            //_produto.MarkUp = Convert.ToDouble(_row["markUp"]);
            _produto.Desconto = 5;
            //_produto.Frete = Convert.ToDouble(_row["frete"]);
            //_produto.Peso = Convert.ToInt32(_row["peso"]);
            _produto.Estoque = 2;

            //_produto.DtCadastro = Convert.ToDateTime(_row["dtCadastro"]);
            //_produto.ExibirHome = Convert.ToBoolean(_row["exibirHome"]);
            //_produto.Destaque = Convert.ToBoolean(_row["destaque"]);
            //_produto.Ativo = Convert.ToBoolean(_row["ativo"]);

            ////Dados do distribuidor
            //_produto.Distribuidor.ID = Convert.ToInt32(_row["idDistribuidor"]);
            //_produto.Distribuidor.Nome = Convert.ToString(_row["nomeDistribuidor"]);

            //Dados da categoria
            _produto.Categoria.ID = 5;
            _produto.Categoria.IDCategoriaPai = 10;
            _produto.Categoria.Titulo = "Titulo Categoria";
            _produto.Categoria.TituloCategoriaPai = "Titulo Categoria Pai";

            ProdutoImagemOT _imagem;
            _imagem = new ProdutoImagemOT
            {
                ID = 10,
                Titulo = "produto-01-thumb.jpg"
            };
            _produto.Imagens.Add(_imagem);

            _imagem = new ProdutoImagemOT
            {
                ID = 20,
                Titulo = "produto-02-thumb.jpg"
            };
            _produto.Imagens.Add(_imagem);

            ProdutoVideoOT _video;
            _video = new ProdutoVideoOT
            {
                ID = 10,
                Titulo = @"//www.youtube.com/embed/zpOULjyy-n8?rel=0"
            };
            _produto.Videos.Add(_video);

            return _produto;
        }
    }
}