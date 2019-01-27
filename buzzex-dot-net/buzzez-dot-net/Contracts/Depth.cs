// -----------------------------------------------------------------------------
// <copyright file="Depth" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:12:11 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;
    using System.Collections.Generic;

    #endregion Usings

    public class Depth
    {
        #region Properties

        [JsonProperty(PropertyName = "asks")]
        public List<List<decimal>> Asks { get; set; }

        [JsonProperty(PropertyName = "bids")]
        public List<List<decimal>> Bids { get; set; }

        #endregion Properties
    }
}