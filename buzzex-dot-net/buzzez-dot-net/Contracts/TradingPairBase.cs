// -----------------------------------------------------------------------------
// <copyright file="TradingPairBase" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 7:52:43 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class TradingPairBase
    {
        #region Properties

        [JsonProperty(PropertyName = "decimal_places")]
        public int DecimalPlaces { get; set; }

        [JsonProperty(PropertyName = "min_price")]
        public decimal MinPrice { get; set; }

        [JsonProperty(PropertyName = "max_price")]
        public decimal MaxPrice { get; set; }

        [JsonProperty(PropertyName = "min_amount")]
        public decimal MinAmount { get; set; }

        [JsonProperty(PropertyName = "min_total")]
        public decimal MinTotal { get; set; }

        [JsonProperty(PropertyName = "hidden")]
        public int Hidden { get; set; }

        [JsonProperty(PropertyName = "fee")]
        public decimal Fee { get; set; }

        [JsonProperty(PropertyName = "fee_buyer")]
        public decimal FeeBuyer { get; set; }

        [JsonProperty(PropertyName = "fee_seller")]
        public decimal FeeSeller { get; set; }

        #endregion Properties
    }
}