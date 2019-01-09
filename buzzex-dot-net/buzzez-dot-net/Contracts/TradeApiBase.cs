// -----------------------------------------------------------------------------
// <copyright file="TradeApiBase" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:24:51 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class TradeApiBase
    {
        #region Properties

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        #endregion Properties
    }
}