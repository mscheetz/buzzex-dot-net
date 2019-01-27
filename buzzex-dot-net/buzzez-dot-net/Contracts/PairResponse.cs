// -----------------------------------------------------------------------------
// <copyright file="PairResponse" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 10:23:32 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public class PairResponse
    {
        #region Properties

        [JsonProperty(PropertyName = "server_time")]
        public long ServerTime { get; set; }

        [JsonProperty(PropertyName = "pairs")]
        public List<Dictionary<string, TradingPairBase>> Pairs { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public ResponseMeta Meta { get; set; }

        #endregion Properties
    }
}