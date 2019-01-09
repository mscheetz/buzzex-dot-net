// -----------------------------------------------------------------------------
// <copyright file="ResponseMeta" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:01:56 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class ResponseMeta
    {
        #region Properties

        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; set; }

        #endregion Properties
    }
}