// -----------------------------------------------------------------------------
// <copyright file="CancelOrderResponse" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 11:46:05 AM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class CancelOrderResponse : TradeResponse
    {
        #region Properties

        [JsonProperty(PropertyName = "data")]
        public CancelResponse Data { get; set; }

        #endregion Properties
    }
}