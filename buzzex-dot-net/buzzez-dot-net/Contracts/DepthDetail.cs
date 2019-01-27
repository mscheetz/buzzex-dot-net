// -----------------------------------------------------------------------------
// <copyright file="DepthDetail" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:09:01 PM" />
// -----------------------------------------------------------------------------


namespace buzzez_dot_net.Contracts
{
    #region Usings

    using System.Collections.Generic;
    
    #endregion Usings

    public class DepthDetail
    {
        #region Properties

        public string Pair { get; set; }

        public List<DepthInfo> Asks { get; set; }

        public List<DepthInfo> Bids { get; set; }

        #endregion Properties
    }
}