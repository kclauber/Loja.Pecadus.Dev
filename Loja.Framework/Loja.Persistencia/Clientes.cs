﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Loja.Objeto;
using System.Configuration;

namespace Loja.Persistencia
{
    public class ClientesOP : Persistencia
    {
        /// <summary>
        /// Este método é utilizado para verificar se já existe o cadastro do cliente
        /// </summary>
        public void SelectCliente(ref ClienteOT cliente)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                if (!cliente.ID.Equals(-1) || !String.IsNullOrEmpty(cliente.Email))
                {
                    sql.AppendFormat(@"select id, nome, cep, end, num, compl, bairro, cidade, estado, tel, email, dtCadastro 
                                   from clientes
                                   where {0} = '{1}'",
                                       (!cliente.ID.Equals(-1) ? "id" : "email"),
                                       cliente.Email);

                    reader = ExecutarReader(sql.ToString());
                    if (reader.Read())
                    {
                        cliente.ID = Convert.ToInt32(reader["id"]);
                        cliente.Nome = (string)reader["nome"];
                        cliente.CEP = (string)reader["cep"];
                        cliente.Endereco = (string)reader["end"];
                        cliente.Numero = (string)reader["num"];
                        cliente.Complemento = (string)reader["compl"];
                        cliente.Bairro = (string)reader["bairro"];
                        cliente.Cidade = (string)reader["cidade"];
                        cliente.Estado = (string)reader["estado"];
                        cliente.Telefone = (string)reader["tel"];
                        cliente.Email = (string)reader["email"];
                        cliente.DtCadastro = (DateTime)reader["dtCadastro"];
                    }
                    else
                        cliente = null;
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
        /// <summary>
        /// Este método retorna um objeto cliente completo buscando pelo ID
        /// </summary>
        
        public void CriarCliente(ref ClienteOT cliente)
        {
            //Verifica se o cliente já existe
            SelectCliente(ref cliente);
            if (cliente.ID.Equals(-1))
                InsertCliente(ref cliente); //Se não existe insere na base
            else
                UpdateCliente(ref cliente);//Se já existe atualiza a base 


        }
        public void InsertCliente(ref ClienteOT cliente)
        {
            StringBuilder sql = new StringBuilder();

            try
            {
                sql.AppendFormat(@"insert into clientes 
                                        (nome, cep, end, num, compl, bairro, cidade, estado, tel, email, dtCadastro)
                                   values
                                        ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', now())",
                                    cliente.Nome,
                                    cliente.CEP,
                                    cliente.Endereco,
                                    cliente.Numero,
                                    cliente.Complemento,
                                    cliente.Bairro,
                                    cliente.Cidade,
                                    cliente.Estado,
                                    cliente.Telefone,
                                    cliente.Email);

                ExecutarNonQuery(sql.ToString());

                SelectCliente(ref cliente);
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
        public void UpdateCliente(ref ClienteOT cliente)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendFormat(@"update clientes set 
                                          nome = '{1}',
                                          cep  = '{2}',
                                          end  = '{3}',
                                          num  = '{4}',
                                          compl  = '{5}',
                                          bairro = '{6}',
                                          cidade = '{7}',
                                          estado = '{8}',
                                          tel    = '{9}'
                                          where id = {0}",
                                 cliente.ID.ToString(),
                                 cliente.Nome,
                                 cliente.CEP,
                                 cliente.Endereco,
                                 cliente.Numero,
                                 cliente.Complemento,
                                 cliente.Bairro,
                                 cliente.Cidade,
                                 cliente.Estado,
                                 cliente.Telefone);

                ExecutarNonQuery(sql.ToString());

                SelectCliente(ref cliente);
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