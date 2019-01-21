using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using Loja.Objeto;
using Loja.Persistencia;
using Loja.Util;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace Loja.UI.Pecadus
{
    public partial class ProcessaRetornoPagSeguro : System.Web.UI.Page
    {
        bool isSandbox = bool.Parse(ConfigurationManager.AppSettings["isSandbox"]);
        string Result = "";
        string Dados = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "https://sandbox.pagseguro.uol.com.br");

            if (!Page.IsPostBack)
            {
                string errMsg = "";
                string notificationType = Request["notificationType"];
                string notificationCode = Request["notificationCode"];

                try
                {
                    if (!String.IsNullOrEmpty(notificationCode) &&
                        !String.IsNullOrEmpty(PagSeguroConfiguration.Credentials(isSandbox).Email) &&
                        !String.IsNullOrEmpty(PagSeguroConfiguration.Credentials(isSandbox).Token))
                    {
                        AccountCredentials credentials = new AccountCredentials(
                            PagSeguroConfiguration.Credentials(isSandbox).Email,
                            PagSeguroConfiguration.Credentials(isSandbox).Token
                        );

                        Transaction transaction = NotificationService.CheckTransaction(
                            credentials,
                            notificationCode,
                            false
                        );

                        //Passa os dados da transação para um TXT
                        LogarRetorno(notificationType, notificationCode, credentials, transaction);

                        //Insere os dados da transação na base de dados
                        ProcessarRetorno(notificationType, notificationCode, credentials, transaction);
                    }
                }
                catch (Exception ex)
                {
                    errMsg += "<b>Erro de processamento no retorno do PS:</b><br>" +
                              "<b>Message</b>=>" + ex.Message + "<br>" +
                              "<b>Source</b>=>" + ex.Source + "<br>" +
                              "<b>GetType</b>=>" + ex.GetType() + "<br>" +
                              "<b>StackTrace</b>=>" + ex.StackTrace + "<br><br>" +
                              "<b>GetBaseException</b>=>" + ex.GetBaseException() + "<br>";

                    new Utilitarios().EnviarEmail("erro ps", errMsg, 
                                                  ConfigurationManager.AppSettings["mailServidor"],
                                                  ConfigurationManager.AppSettings["mailAdmin"]);
                    throw;
                }
            }
        }

        #region ## Métodos com o framework PagSeguro ##
        private void ProcessarRetorno(string notificationType, string notificationCode, AccountCredentials credentials, Transaction transaction)
        {
                //Carregando os objetos internos
                ClienteOT cliente = CriarCliente(transaction);

                //Criando o pedido na base
                new PedidosOP().CriarPedido(ref cliente);

                //Enviando e-mail para o usuário com detalhes da transação
                NotifcarUsuario(transaction);
        }
        private void LogarRetorno(string notificationType, string notificationCode, AccountCredentials credentials, Transaction transaction)
        {
            string file = "";
            StreamWriter sw = null;
            try
            {
                file = Server.MapPath(String.Format("~/Transactions/{0}.txt", transaction.Code));

                sw = new StreamWriter(file, true, System.Text.Encoding.GetEncoding("iso-8859-1"));
                sw.WriteLine("::::::::::::::::::::::::::::::::");
                sw.WriteLine("===> {0} - Início da transação", DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
                sw.WriteLine("===> notificationType: {0}", notificationType);

                //Verificando as informações básicas para fazer a requisição
                sw.WriteLine("===> notificationCode: {0}", notificationCode);

                sw.WriteLine("===> Chamada do serviço >>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("===> credentials.Email: {0}", PagSeguroConfiguration.Credentials(isSandbox).Email);
                sw.WriteLine("===> credentials.Token: {0}", PagSeguroConfiguration.Credentials(isSandbox).Token);
                sw.WriteLine("===>>>>>>>>>>>>>>>>>>>>>");

                //Resumo da transação
                sw.WriteLine("===> Resumo da transação: {0}", transaction.ToString());
                sw.WriteLine("::::::::::::::::::::::::::::::::");
                sw.WriteLine("");

                #region ## Transação ##
                //Listando todos os detalhes da transação
                sw.WriteLine("===> Detalhes da transação >>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("===> Reference: {0}", transaction.Reference);
                sw.WriteLine("===> Code: {0}", transaction.Code);
                sw.WriteLine("===> Date: {0}", transaction.Date.ToString("dd/MM/yyyy H:mm:ss"));
                sw.WriteLine("===> LastEventDate: {0}", transaction.LastEventDate.ToString("dd/MM/yyyy H:mm:ss"));
                sw.WriteLine("===> TransactionType: ({0}){1}", transaction.TransactionType,
                                                               transaction.TransactionTypeDescription);

                sw.WriteLine("===> TransactionStatus: ({0}){1} <<<<============", transaction.TransactionStatus,
                                                                                  transaction.TransactionStatusDescription);
                sw.WriteLine("===>>>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("");
                #endregion
                #region ## Pagamento ##
                sw.WriteLine("===> Detalhes da pagamento >>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("===> PaymentMethod.PaymentMethodType: ({0}){1}", transaction.PaymentMethod.PaymentMethodType,
                                                                               transaction.PaymentMethod.PaymentMethodTypeDescription);
                sw.WriteLine("===> PaymentMethod.PaymentMethodCode: ({0}){1}", transaction.PaymentMethod.PaymentMethodCode,
                                                                               transaction.PaymentMethod.PaymentMethodCodeDescription);

                sw.WriteLine("===> GrossAmount: {0:R$ #,##0.00}", transaction.GrossAmount);
                sw.WriteLine("===> DiscountAmount: {0:R$ #,##0.00}", transaction.DiscountAmount);
                sw.WriteLine("===> FeeAmount: {0:R$ #,##0.00}", transaction.FeeAmount);
                sw.WriteLine("===> NetAmount: {0:R$ #,##0.00}", transaction.NetAmount);
                sw.WriteLine("===> ExtraAmount: {0:R$ #,##0.00}", transaction.ExtraAmount);
                sw.WriteLine("===> InstallmentCount: {0}", transaction.InstallmentCount);
                sw.WriteLine("===>>>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("");
                #endregion
                #region ## Comprador ##
                sw.WriteLine("===> Comprador >>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("===> Sender.Name: {0}", transaction.Sender.Name);
                sw.WriteLine("===> Sender.Email: {0}", transaction.Sender.Email);
                sw.WriteLine("===> Sender.Phone: {0}-{1}", transaction.Sender.Phone.AreaCode,
                                                           transaction.Sender.Phone.Number);
                sw.WriteLine("===> Sender.Documents >>>>>>>>>>>>>>>>>>>>");
                foreach (SenderDocument doc in transaction.Sender.Documents)
                    sw.WriteLine("===> Sender.Documents: {0}-{1}", doc.Type, doc.Value);
                sw.WriteLine("===>>>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("");
                #endregion
                #region ## Items ##
                sw.WriteLine("===> Items >>>>>>>>>>>>>>>>>>>>");
                foreach (Item item in transaction.Items)
                {
                    sw.WriteLine("===> Items.Id: {0}", item.Id);
                    sw.WriteLine("===> Items.Description: {0}", item.Description);
                    sw.WriteLine("===> Items.Amount: {0:R$ #,##0.00}", item.Amount);
                    sw.WriteLine("===> Items.Quantity: {0}", item.Quantity);
                    sw.WriteLine("===> Items.ShippingCost: {0:R$ #,##0.00}", item.ShippingCost);
                    sw.WriteLine("===> Items.Weight: {0:0.000}", item.Weight);
                    sw.WriteLine("===-------------------");
                }
                sw.WriteLine("===>>>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("");
                #endregion
                #region ## Detalhes do envio ##
                sw.WriteLine("===> Detalhes do envio >>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("===> Shipping.ShippingType: ({0}){1}", transaction.Shipping.ShippingType.Value,
                                                                     transaction.Shipping.ShippingTypeDescription);
                sw.WriteLine("===> Shipping.Cost: {0:R$ #,##0.00}", transaction.Shipping.Cost);
                sw.WriteLine("===>>>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("");
                #endregion
                #region ## Endereço de entrega ##
                sw.WriteLine("===> Endereço de entrega >>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("===> Shipping.Address.Country: {0}", transaction.Shipping.Address.Country);
                sw.WriteLine("===> Shipping.Address.State: {0}", transaction.Shipping.Address.State);
                sw.WriteLine("===> Shipping.Address.City: {0}", transaction.Shipping.Address.City);
                sw.WriteLine("===> Shipping.Address.District: {0}", transaction.Shipping.Address.District);
                sw.WriteLine("===> Shipping.Address.PostalCode: {0}", transaction.Shipping.Address.PostalCode);
                sw.WriteLine("===> Shipping.Address.Street: {0}", transaction.Shipping.Address.Street);
                sw.WriteLine("===> Shipping.Address.Number: {0}", transaction.Shipping.Address.Number);
                sw.WriteLine("===> Shipping.Address.Complement: {0}", transaction.Shipping.Address.Complement);
                sw.WriteLine("===>>>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("");
                #endregion
                #region ## PreApproval ##
                sw.WriteLine("===> PreApproval >>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("===> Name: {0}", transaction.Name);
                sw.WriteLine("===> Tracker: {0}", transaction.Tracker);
                sw.WriteLine("===> Status: {0}", transaction.Status);
                sw.WriteLine("===> Charge: {0}", transaction.Charge);
                sw.WriteLine("===>>>>>>>>>>>>>>>>>>>>>");
                sw.WriteLine("");
                #endregion

                sw.WriteLine("===> {0} - Fim da transação", DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
                sw.WriteLine("::::::::::::::::::::::::::::::::");
                sw.WriteLine("");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
        }
        private ClienteOT CriarCliente(Transaction transaction)
        {
            ClienteOT cliente = new ClienteOT()
            {
                Nome = transaction.Sender.Name,
                Email = transaction.Sender.Email,
                Endereco = transaction.Shipping.Address.Street,
                Numero = transaction.Shipping.Address.Number,
                Complemento = transaction.Shipping.Address.Complement,
                Bairro = transaction.Shipping.Address.District,
                Cidade = transaction.Shipping.Address.City,
                Estado = transaction.Shipping.Address.State,
                CEP = transaction.Shipping.Address.PostalCode,
                Telefone = String.Format("{0}{1}", transaction.Sender.Phone.AreaCode, transaction.Sender.Phone.Number)
            };

            PedidoOT pedido = new PedidoOT()
            {
                TransacaoID = transaction.Code,
                Extras = Convert.ToDouble(transaction.ExtraAmount),
                TipoFrete = transaction.Shipping.ShippingTypeDescription,
                ValorFrete = Convert.ToDouble(transaction.Shipping.Cost),
                Anotacao = "",
                TipoPagamento = transaction.PaymentMethod.PaymentMethodTypeDescription,
                Status = transaction.TransactionStatusDescription,
                Parcelas = transaction.InstallmentCount
            };

            //Itens do pedido
            ProdutoOT prod;
            foreach (Item ti in transaction.Items)
            {
                prod = new ProdutoOT()
                {
                    ID = Convert.ToInt32(ti.Id),
                    QuantidadeCarrinho = ti.Quantity,
                    Preco = Convert.ToDouble(ti.Amount)
                };
                pedido.Produtos.Add(prod);
            }
            cliente.Pedidos.Add(pedido);

            return cliente;
        }
        private void NotifcarUsuario(Transaction transaction)
        {
            #region ## html com os detalhes dos produtos do pedido ##
            string detalhesProdutos = "<table style='border: silver solid 1px; width:350px;'><tr><td>";
            int count = 0;
            foreach (Item item in transaction.Items)
            {
                if(count > 0)
                    detalhesProdutos += "<hr style='border:0; border-top: silver solid 1px;'>";
                
                detalhesProdutos +=
                    String.Format(@"Produto: Ref.{0:00000} - {1}<br/>
                                    Preço: {2:R$ #,##0.00}<br/>
                                    Quantidade: {3}<br/>",
                                    item.Id,
                                    item.Description,
                                    item.Amount,
                                    item.Quantity);
                count++;
            }
            detalhesProdutos += "</td></tr></table>";
            #endregion

            //Variáveis para o envio de e-mail
            string assunto = "";
            string mensagem = "";
            string emailTo = "";

            ///Status da transação:
            ///1 Aguardando pagamento.
            ///2 Em análise.
            ///3 Paga.
            ///4 Disponível.
            ///5 Em disputa.
            ///6 Devolvida.
            ///7 Cancelada.
            ///8 Chargeback debitado.
            ///9 Em contestação.
            
            emailTo = transaction.Sender.Email;

            switch (transaction.TransactionStatus)
            {
                //Enviar e-mail de confirmação de pedido
                case 1:
                case 2:
                    assunto = String.Format("{0}: Obrigado pela compra!", 
                                ConfigurationManager.AppSettings["nomeSiteCompleto"]);
                    mensagem =
                    String.Format(@"<p style=""font: 'trebuchet ms';"">Olá <u>{0}</u>, <br/><br/>
                                  Obrigado por comprar na {1}, tenha certeza que você comprou produtos de qualidade.
                                  <br/><br/>
                                  <b>O status atual do seu pedido é: <u>{7}</u></b>.
                                  <br/>
                                  Assim que recebermos a confirmação de pagamento, seu pedido mudará de status e você será avisado por e-mail.
                                  <hr/>
                                  <b>Seguem abaixo detalhes do seu pedido:</b><br/>
                                  Quantidade de itens: {2}<br/>
                                  Valor total do pedido: {3}<br/>
                                  Forma de envio: {4}
                                  <br/><br/>
                                  <b>Detalhes dos produtos:</b><br/>
                                  {5}
                                  <br/>
                                  <hr/>
                                  Este é um e-mail automático, não é necessário responde-lo.<br/>
                                  Em caso de dúvida por favor entre em contato com nossa central pelo e-mail: <b>{6}</b>.
                                  </p>",
                                  transaction.Sender.Name,
                                  ConfigurationManager.AppSettings["nomeSiteCompleto"],
                                  transaction.Items.Count,
                                  String.Format("{0:R$ #,##0.00}", transaction.GrossAmount),
                                  transaction.Shipping.ShippingTypeDescription,
                                  detalhesProdutos,
                                  ConfigurationManager.AppSettings["mailPrincipal"],
                                  transaction.TransactionStatusDescription);
                    break;
                //Enviar e-mail de pedido em separação
                case 3:
                    emailTo = transaction.Sender.Email;
                    assunto = String.Format("{0}: Pagamento aprovado!",
                              ConfigurationManager.AppSettings["nomeSiteCompleto"]);

                    mensagem =
                    String.Format(@"<p style=""font: 'trebuchet ms';"">Olá <u>{0}</u>, <br/><br/>
                                  Obrigado por comprar na {1}, tenha certeza que você comprou produtos de qualidade.
                                  <br/><br/>
                                  <b>O status atual do seu pedido é: <u>{7}</u></b>.
                                  <br/>
                                  Seus produtos serão embalados e enviados no prazo de até {8} dias úteis.<br>
                                  Assim que seu pedido for enviado, enviaremos por e-mail o código de rastreio dos correios.
                                  <hr/>
                                  <b>Seguem abaixo detalhes do seu pedido:</b><br/>
                                  Quantidade de itens: {2}<br/>
                                  Valor total do pedido: {3}<br/>
                                  Forma de envio: {4}
                                  <br/><br/>
                                  <b>Detalhes dos produtos:</b><br/>
                                  {5}
                                  <br/>
                                  <hr/>
                                  Este é um e-mail automático, não é necessário responde-lo.<br/>
                                  Em caso de dúvida por favor entre em contato com nossa central pelo e-mail: <b>{6}</b>.
                                  </p>",
                                  transaction.Sender.Name,
                                  ConfigurationManager.AppSettings["nomeSiteCompleto"],
                                  transaction.Items.Count,
                                  String.Format("{0:R$ #,##0.00}", transaction.GrossAmount),
                                  transaction.Shipping.ShippingTypeDescription,
                                  detalhesProdutos,
                                  ConfigurationManager.AppSettings["mailPrincipal"],
                                  "EM SEPARAÇÃO",
                                  ConfigurationManager.AppSettings["prazoPreparacaoEnvio"]);
                    break;
            }

            //Notificando o cliente da mudança de status do pedido
            new Utilitarios().EnviarEmail(assunto,
                                          mensagem,
                                          ConfigurationManager.AppSettings["mailServidor"],
                                          emailTo,//"clauber.oliveira@grupopaodeacucar.com.br",
                                          "",
                                          ConfigurationManager.AppSettings["mailAdmin"]); 
        }
        #endregion
        #region ## Métodos Antigos ##
        [Obsolete]
        protected void oldProcessarRetorno()
        {
            //Result = "VERIFICADO";
            processamentoPagSeguro();

            //Post verificado pelo Pag Seguro
            if (Result == "VERIFICADO")
            {
                //Processando o pedido
                ClienteOT cliente = new ClienteOT();
                CarregaObjetosPedido(ref cliente);
                new PedidosOP().ProcessaPedido(cliente);

                //Esvazia o carrinho
                Carrinho.Instancia.Limpar();

                //Processado com sucesso
                new Utilitarios().EnviarEmail("Retorno do Pag Seguro Processado!!",
                                              Dados.Replace("&", "<br/>"),
                                              ConfigurationManager.AppSettings["mailServidor"],
                                              ConfigurationManager.AppSettings["mailPrincipal"]);
            }
            //Post NÃO verificado pelo Pag seguro
            else if (Result == "FALSO")
            {
                new Utilitarios().EnviarEmail("Retorno não validado pelo Pag seguro!!",
                                              Dados.Replace("&", "<br/>"),
                                              ConfigurationManager.AppSettings["mailServidor"],
                                              ConfigurationManager.AppSettings["mailPrincipal"]);
            }
            //Houve um erro na validação do Pag seguro
            else
            {
                new Utilitarios().EnviarEmail("EXCEÇÃO na validação do Pag seguro",
                                              Dados.Replace("&", "<br/>"),
                                              ConfigurationManager.AppSettings["mailServidor"],
                                              ConfigurationManager.AppSettings["mailPrincipal"]);
            }
        }
        /// <summary>
        /// Tratamento do post PagSeguro //NÃO ALTERAR//
        /// </summary>
        [Obsolete]
        protected void processamentoPagSeguro()
        {
            //Recebendo o retorno automático do Pag seguro
            string Token = "8871EABBBBE04E2FBE374E3B215FBE85";
            string Pagina = "https://pagseguro.uol.com.br/pagseguro-ws/checkout/NPI.jhtml";

            //Corrigindo problema de encoding
            Dados = "";
            foreach (var key in this.Request.Form.AllKeys)
            {
                if (!String.IsNullOrEmpty(key))
                {
                    String value = this.Request.Form[key].ToString();
                    value = HttpUtility.UrlEncode(value, Encoding.GetEncoding("ISO-8859-1"));
                    Dados += String.Format("{0}={1}&", key, value);
                }
            }
            Dados += "Comando=validar&Token=" + Token;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Pagina);
            req.Method = "POST";
            req.ContentLength = Dados.Length;
            req.ContentType = "application/x-www-form-urlencoded";

            //Enviando POST ao Pag seguro
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), Encoding.GetEncoding("ISO-8859-1"));
            stOut.Write(Dados);
            stOut.Close();

            //Recebendo o segundo retorno já validado
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
            Result = stIn.ReadToEnd();
            stIn.Close();
        }
        [Obsolete]
        private void CarregaObjetosPedido(ref ClienteOT cliente)
        {
            //Dados do cliente
            cliente.Nome = (string)Request["CliNome"];
            cliente.Email = (string)Request["CliEmail"];
            cliente.Endereco = (string)Request["CliEndereco"];
            cliente.Numero = (string)Request["CliNumero"];
            cliente.Complemento = (string)Request["CliComplemento"];
            cliente.Bairro = (string)Request["CliBairro"];
            cliente.Cidade = (string)Request["CliCidade"];
            cliente.Estado = (string)Request["CliEstado"];
            cliente.CEP = (string)Request["CliCEP"];
            cliente.Telefone = (string)Request["CliTelefone"];

            //Dados gerais do pedido
            PedidoOT pedido = new PedidoOT();
            pedido.TransacaoID = (string)Request["TransacaoID"];
            pedido.Extras = Convert.ToDouble(Request["Extras"]);
            pedido.TipoFrete = (string)Request["TipoFrete"];
            pedido.ValorFrete = Convert.ToDouble(Request["ValorFrete"]);
            pedido.Anotacao = (string)Request["Anotacao"];
            pedido.TipoPagamento = (string)Request["TipoPagamento"];
            pedido.Status = (string)Request["StatusTransacao"];
            pedido.Parcelas = Convert.ToInt32(Request["Parcelas"]);

            //Itens do pedido
            ProdutoOT prod;
            for (int a = 1; a <= Convert.ToInt32(Request["NumItens"]); a++)
            {
                prod = new ProdutoOT();
                prod.ID = Convert.ToInt32(Request["ProdID_" + a.ToString()]);
                prod.QuantidadeCarrinho = Convert.ToInt32(Request["ProdQuantidade_" + a.ToString()]);
                prod.Preco = Convert.ToDouble(Request["ProdValor_" + a.ToString()]);
                pedido.Produtos.Add(prod);
            }
            cliente.Pedidos.Add(pedido);
        }
        #endregion
    }
}