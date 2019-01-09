// -----------------------------------------------------------------------------
// <copyright file="ResponseBase" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:05:20 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class ResponseBase
    {
        #region Properties

        [JsonProperty(PropertyName = "meta")]
        public ResponseMeta Meta { get; set; }

        #endregion Properties
    }
}