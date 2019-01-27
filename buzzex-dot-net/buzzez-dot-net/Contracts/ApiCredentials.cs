// -----------------------------------------------------------------------------
// <copyright file="ApiCredentials" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/26/2019 7:59:34 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class ApiCredentials
    {
        #region Properties

        [JsonProperty(PropertyName = "apiKey")]
        public string ApiKey { get; set; }

        [JsonProperty(PropertyName = "apiSecret")]
        public string ApiSecret { get; set; }

        #endregion Properties
    }
}