// -----------------------------------------------------------------------------
// <copyright file="OrderInfoResponse" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:26:16 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;
    using System.Collections.Generic;

    #endregion Usings

    public class OrderInfoResponse : TradeApiBase
    {
        #region Properties

        [JsonProperty(PropertyName = "orderInfo")]
        public Dictionary<string, OrderBase> Order { get; set; }

        #endregion Properties
    }
}