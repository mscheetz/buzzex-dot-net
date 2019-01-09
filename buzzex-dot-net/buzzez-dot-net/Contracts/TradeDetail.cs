// -----------------------------------------------------------------------------
// <copyright file="TradeDetail" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:31:42 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    using Newtonsoft.Json;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public class TradeDetail : Trade
    {
        #region Properties

        [JsonProperty(PropertyName = "pair")]
        public string Pair { get; set; }

        [JsonProperty(PropertyName = "order_id")]
        public long OrderId { get; set; }

        [JsonProperty(PropertyName = "is_your_order")]
        public int IsYourOrder { get; set; }

        #endregion Properties
    }
}