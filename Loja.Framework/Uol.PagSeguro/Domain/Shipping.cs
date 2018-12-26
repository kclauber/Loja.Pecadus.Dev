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
    /// Shipping information
    /// </summary>
    public class Shipping
    {
        /// <summary>
        /// Initializes a new instance of the Shipping class
        /// </summary>
        public Shipping()
        {
        }

        /// <summary>
        /// Shipping address
        /// </summary>
        public Address Address
        {
            get;
            set;
        }

        /// <summary>
        /// Tipo de frete: 
        ///1	Encomenda normal (PAC).
        ///2	SEDEX.
        ///3	Tipo de frete não especificado.
        /// </summary>
        public int? ShippingType
        {
            get;
            set;
        }
        /// <summary>
        /// Tipo de frete: 
        ///1	Encomenda normal (PAC).
        ///2	SEDEX.
        ///3	Tipo de frete não especificado.
        /// </summary>
        public string ShippingTypeDescription
        {
            get
            {
                switch (ShippingType.Value)
                {
                    case 1: return "Encomenda normal (PAC)";
                    case 2: return "SEDEX";
                    case 3: return "Tipo de frete não especificado";
                    default: return "";
                }
            }
        }

        /// <summary>
        /// Custo total do frete. This is a read-only property and it is calculated by PagSeguro 
        /// based on the shipping information provided with the payment request.
        /// </summary>
        public decimal? Cost
        {
            get;
            set;
        }
    }
}
