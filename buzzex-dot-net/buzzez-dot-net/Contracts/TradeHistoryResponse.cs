// -----------------------------------------------------------------------------
// <copyright file="TradeHistoryResponse" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:33:19 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class TradeHistoryResponse : TradeApiBase
    {
        #region Properties

        [JsonProperty(PropertyName = "data")]
        public TradeDetail[] Data { get; set; }

        #endregion Properties
    }
}