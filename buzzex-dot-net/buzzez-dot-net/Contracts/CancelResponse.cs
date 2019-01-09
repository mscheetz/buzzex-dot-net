// -----------------------------------------------------------------------------
// <copyright file="CancelResponse" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:34:42 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class CancelResponse
    {
        #region Properties

        [JsonProperty(PropertyName = "order_id")]
        public long OrderId { get; set; }

        [JsonProperty(PropertyName = "funds")]
        public decimal[] Funds { get; set; }

        #endregion Properties
    }
}