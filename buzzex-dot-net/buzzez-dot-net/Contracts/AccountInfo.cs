// -----------------------------------------------------------------------------
// <copyright file="AccountInfo" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:36:00 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    using Newtonsoft.Json;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public class AccountInfo
    {
        #region Properties

        [JsonProperty(PropertyName = "funds")]
        public decimal[] Funds { get; set; }

        [JsonProperty(PropertyName = "funds_incl_orders")]
        public decimal[] FundsLocked { get; set; }

        [JsonProperty(PropertyName = "server_time")]
        public long ServerTime { get; set; }

        #endregion Properties
    }
}