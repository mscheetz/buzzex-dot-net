// -----------------------------------------------------------------------------
// <copyright file="TickerBase" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:06:12 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class TickerBase
    {
        #region Properties

        [JsonProperty(PropertyName = "pair_id")]
        public int PairId { get; set; }

        [JsonProperty(PropertyName = "last")]
        public decimal Last { get; set; }

        [JsonProperty(PropertyName = "lowest_ask")]
        public decimal Low { get; set; }

        [JsonProperty(PropertyName = "highest_bid")]
        public decimal High { get; set; }

        [JsonProperty(PropertyName = "price_24h")]
        public decimal Price24h { get; set; }

        [JsonProperty(PropertyName = "base_volume")]
        public decimal BaseVolume { get; set; }

        [JsonProperty(PropertyName = "quote_volume")]
        public decimal QuoteVolume { get; set; }

        [JsonProperty(PropertyName = "is_frozen")]
        public decimal IsFrozen { get; set; }

        [JsonProperty(PropertyName = "high_24hr")]
        public decimal High24h { get; set; }

        [JsonProperty(PropertyName = "low_24hr")]
        public decimal Low24h { get; set; }

        [JsonProperty(PropertyName = "percent_change")]
        public decimal PercentChange { get; set; }

        [JsonProperty(PropertyName = "updated")]
        public long Updated { get; set; }

        #endregion Properties
    }
}