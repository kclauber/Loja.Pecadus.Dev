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


namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Payment method
    /// </summary>
    public class PaymentMethod
    {
        /// <summary>
        /// Payment method type
        /// </summary>
        public int PaymentMethodType
        {
            get;
            set;
        }
        /// <summary>
        ///Tipo do meio de pagamento:
        ///1	Pagamento.
        ///2	Boleto.
        ///3	Débito online (TEF).
        ///4	Saldo PagSeguro.
        ///5	Oi Paggo *.
        ///7	Depósito em conta.
        /// </summary>
        /// <remarks>
        /// * Os tipos marcados não estão disponíveis para utilização.
        /// </remarks>
        public string PaymentMethodTypeDescription
        {
            get
            {
                switch (PaymentMethodType)
                {
                    case 1: return "Pagamento";
                    case 2: return "Boleto";
                    case 3: return "Débito online (TEF)";
                    case 4: return "Saldo PagSeguro";
                    case 5: return "Oi Paggo";
                    case 7: return "Depósito em conta";//o comprador optou por fazer um depósito na conta corrente do PagSeguro
                    default: return "";
                }
            }
        }

        /// <summary>
        /// Payment method code
        /// </summary>
        public int PaymentMethodCode
        {
            get;
            set;
        }
        /// <summary>
        ///Código identificador do meio de pagamento:
        ///101	Cartão de crédito Visa.
        ///102	Cartão de crédito MasterCard.
        ///103	Cartão de crédito American Express.
        ///104	Cartão de crédito Diners.
        ///105	Cartão de crédito Hipercard.
        ///106	Cartão de crédito Aura.
        ///107	Cartão de crédito Elo.
        ///108	Cartão de crédito PLENOCard.
        ///109	Cartão de crédito PersonalCard.
        ///110	Cartão de crédito JCB.
        ///111	Cartão de crédito Discover.
        ///112	Cartão de crédito BrasilCard.
        ///113	Cartão de crédito FORTBRASIL.
        ///114	Cartão de crédito CARDBAN.
        ///115	Cartão de crédito VALECARD.
        ///116	Cartão de crédito Cabal.
        ///117	Cartão de crédito Mais!.
        ///118	Cartão de crédito Avista.
        ///119	Cartão de crédito GRANDCARD.
        ///120	Cartão de crédito Sorocred.
        ///201	Boleto Bradesco. *
        ///202	Boleto Santander.
        ///301	Débito online Bradesco.
        ///302	Débito online Itaú.
        ///303	Débito online Unibanco. *
        ///304	Débito online Banco do Brasil.
        ///305	Débito online Banco Real. *
        ///306	Débito online Banrisul.
        ///307	Débito online HSBC.
        ///401	Saldo PagSeguro.
        ///501	Oi Paggo. *
        ///701	Depósito em conta - Banco do Brasil
        ///702	Depósito em conta - HSBC
        /// </summary>
        /// <remarks>
        /// * Os tipos marcados não estão disponíveis para utilização.
        /// </remarks>
        public string PaymentMethodCodeDescription
        {
            get
            {
                switch (PaymentMethodCode)
                {
                    case 101: return "Cartão de crédito Visa";
                    case 102: return "Cartão de crédito MasterCard";
                    case 103: return "Cartão de crédito American Express";
                    case 104: return "Cartão de crédito Diners";
                    case 105: return "Cartão de crédito Hipercard";
                    case 106: return "Cartão de crédito Aura";
                    case 107: return "Cartão de crédito Elo";
                    case 108: return "Cartão de crédito PLENOCard";
                    case 109: return "Cartão de crédito PersonalCard";
                    case 110: return "Cartão de crédito JCB";
                    case 111: return "Cartão de crédito Discover";
                    case 112: return "Cartão de crédito BrasilCard";
                    case 113: return "Cartão de crédito FORTBRASIL";
                    case 114: return "Cartão de crédito CARDBAN";
                    case 115: return "Cartão de crédito VALECARD";
                    case 116: return "Cartão de crédito Cabal";
                    case 117: return "Cartão de crédito Mais!";
                    case 118: return "Cartão de crédito Avista";
                    case 119: return "Cartão de crédito GRANDCARD";
                    case 120: return "Cartão de crédito Sorocred";
                    case 201: return "Boleto Bradesco *";
                    case 202: return "Boleto Santander";
                    case 301: return "Débito online Bradesco";
                    case 302: return "Débito online Itaú";
                    case 303: return "Débito online Unibanco *";
                    case 304: return "Débito online Banco do Brasil";
                    case 305: return "Débito online Banco Real *";
                    case 306: return "Débito online Banrisul";
                    case 307: return "Débito online HSBC";
                    case 401: return "Saldo PagSeguro";
                    case 501: return "Oi Paggo *";
                    case 701: return "Depósito em conta - Banco do Brasil";
                    case 702: return "Depósito em conta - HSBC";
                    default: return "";
                }
            }
        }
    }
}
