// -----------------------------------------------------------------------------
// <copyright file="PairInformation" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:00:49 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;
    using System.Collections.Generic;

    #endregion Usings

    public class PairInformation : ResponseBase
    {
        #region Properties

        [JsonProperty(PropertyName = "server_time")]
        public long ServerTime { get; set; }

        [JsonProperty(PropertyName = "pairs")]
        public Dictionary<string, TradingPairBase> Pairs { get; set; }

        #endregion Properties
    }
}