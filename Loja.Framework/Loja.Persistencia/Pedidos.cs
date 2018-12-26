using System;
using Loja.Objeto;
using System.Text;
using System.Configuration;

namespace Loja.Persistencia
{
    public class PedidosOP : Persistencia
    {
        public void CriarPedido(ref ClienteOT cliente)
        {
            //try
            //{
                //Criando o cliente no banco
                new ClientesOP().CriarCliente(ref cliente);

                //Insere o pedido na base de dados
                foreach (PedidoOT pedido in cliente.Pedidos)
                {
                    pedido.IdCliente = cliente.ID;
                    pedido.ID = SelectPedidoByTransacao(pedido);

                    //Se não existe insere na base
                    if (pedido.ID.Equals(-1))
                        InsertPedido(pedido);

                    //Se o pedido foi aprovado, atualiza o estoque
                    if (pedido.Status.ToUpper().Equals("APROVADO"))
                    {
                        //Muda o status para em separação para liberar na tela de picking
                        pedido.Status = "Em separação";
                        pedido.Anotacao = String.Format("Pagamento aprovado dia {0:dd/MM/yyyy H:mm:ss}", DateTime.Now);
                        UpdatePedido(pedido);

                        ProdutosOP produtosOP = new ProdutosOP();
                        foreach (ProdutoOT item in pedido.Produtos)
                            produtosOP.AtualizaEstoque(item.ID, (item.QuantidadeCarrinho * -1));
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, string.Empty));
            //}
            //finally
            //{
            //    fecharConexao();
            //}
        }
        
        [Obsolete]
        public void ProcessaPedido(ClienteOT cliente)
        {
            ClientesOP clientesOP = new ClientesOP();
            try
            {
                //Verifica se o cliente já existe
                clientesOP.SelectCliente(ref cliente);
                if (cliente.ID.Equals(-1))
                    clientesOP.InsertCliente(ref cliente);//Se não existe insere na base
                else
                    clientesOP.UpdateCliente(ref cliente);//Se já existe atualiza a base

                //Verifica se já existe o pedido
                for (int a = 0; a < cliente.Pedidos.Count; a++)
                {
                    cliente.Pedidos[a].IdCliente = cliente.ID;
                    cliente.Pedidos[a].ID = SelectPedidoByTransacao(cliente.Pedidos[a]);
                    if (cliente.Pedidos[a].ID == -1)
                    {
                        //Se não existe insere na base
                        InsertPedido(cliente.Pedidos[a]);
                    }
                    else
                    {
                        UpdatePedido(cliente.Pedidos[a]);//Se já existe atualiza a base
                        //Não atualiza os itens por que, teoricamente, eles nunca mudam
                    }

                    if (cliente.Pedidos[a].Status.ToUpper().Equals("AUTORIZADO"))
                    {
                        ProdutosOP produtosOP = new ProdutosOP();
                        foreach (ProdutoOT prod in cliente.Pedidos[a].Produtos)
                            produtosOP.AtualizaEstoque(prod.ID, (prod.QuantidadeCarrinho * -1));
                    }
                }

                
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

        public void SelectPedidoByID(ref PedidoOT pedido)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendFormat(@"SELECT id, idCliente, TransacaoID, TipoFrete, ValorFrete, Anotacao, TipoPagamento, Status, Extras, dtCadastro, Parcelas
                                        from pedidos
                                   where id = '{0}'",
                                   pedido.ID);
                reader = ExecutarReader(sql.ToString());
                if (reader.Read())
                {
                    //Pedido
                    pedido.ID = Convert.ToInt32(reader["id"]);
                    pedido.IdCliente = Convert.ToInt32(reader["idCliente"]);
                    pedido.TransacaoID = Convert.ToString(reader["TransacaoID"]);
                    pedido.TipoFrete = Convert.ToString(reader["TipoFrete"]);
                    pedido.ValorFrete = Convert.ToDouble(reader["ValorFrete"]);
                    pedido.Anotacao = Convert.ToString(reader["Anotacao"]);
                    pedido.TipoPagamento = Convert.ToString(reader["TipoPagamento"]);
                    pedido.Status = Convert.ToString(reader["Status"]);
                    pedido.Extras = Convert.ToDouble(reader["Extras"]);
                    pedido.DtCadastro = Convert.ToDateTime(reader["dtCadastro"]);
                    pedido.Parcelas = Convert.ToInt32(reader["Parcelas"]);

                    //Itens do pedido
                    sql = new StringBuilder();
                    sql.AppendFormat(@"SELECT item.id, 
                                          item.idPedido, 
                                          item.idProduto, 
                                          prod.titulo,
                                          item.quantidade, 
                                          item.valor
                                    FROM pedidosItens item inner join produtos prod
                                    on item.idProduto = prod.id
                                    where item.idPedido = {0}",
                                              pedido.ID);
                    reader = ExecutarReader(sql.ToString());
                    while (reader.Read())
                    {
                        ProdutoOT prod = new ProdutoOT();
                        prod.ID = Convert.ToInt32(reader["id"]);
                        prod.ID = Convert.ToInt32(reader["idProduto"]);
                        prod.Titulo = reader["titulo"].ToString();
                        prod.QuantidadeCarrinho = Convert.ToInt32(reader["quantidade"]);
                        prod.Preco = Convert.ToDouble(reader["valor"]);

                        pedido.Produtos.Add(prod);
                    }
                }
                else
                    pedido = null;
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
        public int SelectPedidoByTransacao(PedidoOT pedido)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendFormat(@"SELECT id, idCliente, TransacaoID, TipoFrete, ValorFrete, Anotacao, TipoPagamento, Status, Extras, dtCadastro, Parcelas
                                   from pedidos
                                   where TransacaoID = '{0}' ",
                                         pedido.TransacaoID);
                reader = ExecutarReader(sql.ToString());
                if (reader.Read())
                {
                    //Pedido
                    return Convert.ToInt32(reader["id"]);
                }
                else
                    return -1;
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
        public void SelectPedidoCliente(ref ClienteOT cliente)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendFormat(@"SELECT id, nome, cep, end, num, compl, bairro, cidade, estado, tel, email, dtCadastro 
                                        FROM clientes 
                                   where id = {0} ",
                                   cliente.ID);
                reader = ExecutarReader(sql.ToString());

                if (reader.Read())
                {
                    //Cliente
                    cliente.ID = Convert.ToInt32(reader["id"]);
                    cliente.Nome = Convert.ToString(reader["nome"]);
                    cliente.CEP = Convert.ToString(reader["cep"]);
                    cliente.Endereco = Convert.ToString(reader["end"]);
                    cliente.Numero = Convert.ToString(reader["num"]);
                    cliente.Complemento = Convert.ToString(reader["compl"]);
                    cliente.Bairro = Convert.ToString(reader["bairro"]);
                    cliente.Cidade = Convert.ToString(reader["cidade"]);
                    cliente.Estado = Convert.ToString(reader["estado"]);
                    cliente.Telefone = Convert.ToString(reader["tel"]);
                    cliente.Email = Convert.ToString(reader["email"]);
                    cliente.DtCadastro = Convert.ToDateTime(reader["dtCadastro"]);

                    //Pedidos
                    sql = new StringBuilder();
                    sql.AppendFormat(@"SELECT id, idCliente, TransacaoID, TipoFrete, ValorFrete, Anotacao, TipoPagamento, Status, Extras, dtCadastro, Parcelas
                                            from pedidos 
                                       where idCliente = {0} ",
                                       cliente.ID);
                    reader = ExecutarReader(sql.ToString());
                    while (reader.Read())
                    {
                        PedidoOT pedido = new PedidoOT();
                        pedido.ID = Convert.ToInt32(reader["id"]);
                        pedido.IdCliente = Convert.ToInt32(reader["idCliente"]);
                        pedido.TransacaoID = Convert.ToString(reader["TransacaoID"]);
                        pedido.TipoFrete = Convert.ToString(reader["TipoFrete"]);
                        pedido.ValorFrete = Convert.ToDouble(reader["ValorFrete"]);
                        pedido.Anotacao = Convert.ToString(reader["Anotacao"]);
                        pedido.TipoPagamento = Convert.ToString(reader["TipoPagamento"]);
                        pedido.Parcelas = Convert.ToInt32(reader["Parcelas"]);
                        pedido.Status = Convert.ToString(reader["Status"]);
                        pedido.Extras = Convert.ToDouble(reader["Extras"]);
                        pedido.DtCadastro = Convert.ToDateTime(reader["dtCadastro"]);
                        cliente.Pedidos.Add(pedido);

                        //Itens dos pedidos
                        sql = new StringBuilder();
                        sql.AppendFormat(@"select a.id, a.idPedido, b.titulo, b.ean, a.idProduto, a.quantidade, a.valor, b.estoque  
                                                from pedidosItens a join produtos b on a.idProduto = b. id 
                                           where idPedido = {0}",
                                           pedido.ID);
                        reader = ExecutarReader(sql.ToString());
                        while (reader.Read())
                        {
                            ProdutoOT prod = new ProdutoOT();
                            prod.ID = Convert.ToInt32(reader["id"]);
                            prod.Titulo = Convert.ToString(reader["titulo"]);
                            prod.EAN = Convert.ToString(reader["ean"]);

                            prod.ID = Convert.ToInt32(reader["idProduto"]);
                            prod.QuantidadeCarrinho = Convert.ToInt32(reader["quantidade"]);
                            prod.Estoque = Convert.ToInt32(reader["estoque"]);
                            prod.Preco = Convert.ToDouble(reader["valor"]);
                            pedido.Produtos.Add(prod);
                        }
                    }
                }
                else
                    cliente = null;
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
        public string getStringSqlPedidos(string status, DateTime dataDe, DateTime dataAte)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"SELECT a.id, a.idCliente, a.TransacaoID, a.TipoFrete, a.ValorFrete, a.Anotacao, 
                                 a.TipoPagamento, a.Status, a.Extras, a.dtCadastro, a.Parcelas, a.lembreteCobranca,
                                 b.id, b.nome cliente 
                             FROM pedidos a inner join clientes b on a.idCliente = b.id
                             where a.dtCadastro between '{1}' and '{2}' and a.status = '{0}' 
                             ORDER BY a.idCliente, a.id",
                             status,
                             dataDe.AddDays(-7).ToString("yyyy-MM-dd 0:0:0"),
                             dataAte.ToString("yyyy-MM-dd 23:59:59"));
            return sql.ToString();
        }

        private void InsertPedido(PedidoOT pedido)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendFormat(@"insert into pedidos
                                            (idCliente, TransacaoID, TipoFrete, ValorFrete, Anotacao, TipoPagamento, Parcelas, Status, Extras, dtCadastro)
                                       values
                                            ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', now())",
                                         pedido.IdCliente,
                                         pedido.TransacaoID,
                                         pedido.TipoFrete,
                                         pedido.ValorFrete.ToString().Replace(".", "").Replace(",", "."),
                                         pedido.Anotacao,
                                         pedido.TipoPagamento,
                                         pedido.Parcelas.ToString(),
                                         pedido.Status,
                                         pedido.Extras.ToString().Replace(".", "").Replace(",", "."));
                ExecutarNonQuery(sql.ToString());

                //Recuperando o ID do pedido
                sql = new StringBuilder();
                sql.AppendFormat("select id from pedidos where TransacaoID = '{0}'", pedido.TransacaoID);

                reader = ExecutarReader(sql.ToString());
                if (reader.Read())
                {
                    pedido.ID = Convert.ToInt32(reader["id"]);
                    InsertPedidoItens(pedido);
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
        private void InsertPedidoItens(PedidoOT pedido)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                foreach (ProdutoOT item in pedido.Produtos)
                {
                    sql = new StringBuilder();
                    sql.AppendFormat(@"insert into pedidosItens
                                            (idPedido, idProduto, quantidade, valor) 
                                       values
                                            ('{0}', '{1}', '{2}', '{3}')",
                                       pedido.ID,
                                       item.ID,
                                       item.QuantidadeCarrinho,
                                       item.Preco.ToString().Replace(".", "").Replace(",", "."));
                    ExecutarNonQuery(sql.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, sql));
            }
        }
        public void UpdatePedido(PedidoOT pedido)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendFormat(@"update pedidos set 
                                          TipoFrete     = '{1}',
                                          ValorFrete    = '{2}',
                                          Anotacao      = '{3}',
                                          TipoPagamento = '{4}',
                                          Parcelas      = '{5}',
                                          Status        = '{6}',
                                          Extras        = '{7}'
                                          where id      =  {0}",
                                    pedido.ID.ToString(),
                                    pedido.TipoFrete,
                                    pedido.ValorFrete.ToString().Replace(".", "").Replace(",", "."),
                                    pedido.Anotacao,
                                    pedido.TipoPagamento,
                                    pedido.Parcelas,
                                    pedido.Status,
                                    pedido.Extras);
                ExecutarNonQuery(sql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro na camada de persistencia: Ex: {0} --> SQL: {1}", ex.Message, sql));
            }
        }
    }
}