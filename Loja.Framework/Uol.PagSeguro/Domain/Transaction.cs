// Copyright [2011] [PagSeguro Internet Ltda.]
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using System.Collections.Generic;
using System.Text;

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Represents a PagSeguro transaction
    /// </summary>
    public class Transaction
    {
        private IList<Item> items;

        internal Transaction()
        {
        }

        /// <summary>
        /// Data da criação da transação
        /// </summary>
        public DateTime Date
        {
            get;
            internal set;
        }

        /// <summary>
        /// Código identificador da transação
        /// </summary>
        public string Code
        {
            get;
            internal set;
        }

        /// <summary>
        /// Código de referência da transação
        /// </summary>
        /// <remarks>
        /// You can use the reference code to store an identifier so you can 
        /// associate the PagSeguro transaction to a transaction in your system.
        /// </remarks>
        public string Reference
        {
            get;
            internal set;
        }

        /// <summary>
        /// Tipo da transação
        /// </summary>
        public int TransactionType
        {
            get;
            internal set;
        }
        /// <summary>
        ///Tipo da transação:
        ///1	Pagamento.
        ///11	Assinatura - 
        ///Novos tipos podem ser adicionados em versões futuras do serviço.
        /// </summary>
        public string TransactionTypeDescription
        {
            get
            {
                //Detalhes no summary
                switch (TransactionType)
                {
                    case 1: return "Pagamento";
                    case 11: return "Assinatura";
                    default: return "";
                }
            }
        }

        /// <summary>
        /// Status da transação
        /// </summary>
        public int TransactionStatus
        {
            get;
            internal set;
        }
        /// <summary>
        ///Status da transação:
        ///1	Aguardando pagamento.
        ///2	Em análise.
        ///3	Paga.
        ///4	Disponível.
        ///5	Em disputa.
        ///6	Devolvida.
        ///7	Cancelada.
        ///8	Chargeback debitado.
        ///9	Em contestação.
        ///Maiores detalhes nos comentários dentro do método
        /// </summary>
        public string TransactionStatusDescription
        {
            get
            {
                switch (TransactionStatus)
                {
                    case 1: return "Aguardando pagamento";//o comprador iniciou a transação, mas até o momento o PagSeguro não recebeu nenhuma informação sobre o pagamento.
                    case 2: return "Em análise";//o comprador optou por pagar com um cartão de crédito e o PagSeguro está analisando o risco da transação.
                    case 3: return "Aprovado";//(Paga) a transação foi paga pelo comprador e o PagSeguro já recebeu uma confirmação da instituição financeira responsável pelo processamento.
                    case 4: return "Disponível";//a transação foi paga e chegou ao final de seu prazo de liberação sem ter sido retornada e sem que haja nenhuma disputa aberta.
                    case 5: return "Em disputa";//o comprador, dentro do prazo de liberação da transação, abriu uma disputa.
                    case 6: return "Devolvido";//o valor da transação foi devolvido para o comprador.
                    case 7: return "Cancelada";//a transação foi cancelada sem ter sido finalizada.
                    case 8: return "Chargeback debitado";//o valor da transação foi devolvido para o comprador.
                    case 9: return "Em contestação";//o comprador abriu uma solicitação de chargeback junto à operadora do cartão de crédito
                    case 101: return "Em separação";//o pagamento foi aprovado, separando o produto
                    case 102: return "Enviado";//TODO: recuperar este status da API dos correios
                    case 103: return "Entregue";//TODO: recuperar este status da API dos correios
                    default: return "";
                }
            }
        }

        /// <summary>
        /// Tipo do meio de pagamento
        /// </summary>
        public PaymentMethod PaymentMethod
        {
            get;
            internal set;
        }

        /// <summary>
        /// Valor bruto
        /// </summary>
        public decimal GrossAmount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Valor do desconto
        /// </summary>
        public decimal DiscountAmount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Taxa de operação
        /// </summary>
        public decimal FeeAmount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Valor líquido
        /// </summary>
        public decimal NetAmount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Valor extra
        /// </summary>
        public decimal ExtraAmount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Data do último evento
        /// </summary>
        public DateTime LastEventDate
        {
            get;
            internal set;
        }

        /// <summary>
        /// Número de parcelas
        /// </summary>
        public int InstallmentCount
        {
            get;
            internal set;
        }

        /// <summary>
        /// Dados do comprador
        /// </summary>
        /// <remarks>
        /// Who is sending the money
        /// </remarks>
        public Sender Sender
        {
            get;
            internal set;
        }

        /// <summary>
        /// Lista de itens contidos na transação
        /// </summary>
        public IList<Item> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new List<Item>();
                }
                return items;
            }
        }

        /// <summary>
        /// Dados do frete
        /// </summary>
        public Shipping Shipping
        {
            get;
            internal set;
        }

        /// <summary>
        /// Name
        /// </summary>
        /// <remarks>
        /// Name of PreApproval
        /// </remarks>
        public string Name
        {
            get;
            internal set;
        }

        /// <summary>
        /// Tracker
        /// </summary>
        /// <remarks>
        /// Status of PreApproval
        /// </remarks>
        public string Tracker
        {
            get;
            internal set;
        }

        /// <summary>
        /// Status
        /// </summary>
        /// <remarks>
        /// PreApproval Status
        /// </remarks>
        public string Status
        {
            get;
            internal set;
        }

        /// <summary>
        /// charge
        /// </summary>
        /// <remarks>
        /// Manual or Auto
        /// </remarks>
        public string Charge
        {
            get;
            internal set;
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetType().Name);
            builder.Append('(');
            builder.Append("Code=").Append(this.Code).Append(", ");
            builder.Append("Date=").Append(this.Date).Append(", ");
            builder.Append("Reference=").Append(this.Reference).Append(", ");
            builder.Append("TransactionStatus=").Append(this.TransactionStatus).Append(", ");
            string email = this.Sender == null ? null : this.Sender.Email;
            builder.Append("Sender.Email=").Append(email).Append(", ");
            builder.Append("Items.Count=").Append(this.Items.Count);
            builder.Append(')');
            return builder.ToString();
        }
    }
}
