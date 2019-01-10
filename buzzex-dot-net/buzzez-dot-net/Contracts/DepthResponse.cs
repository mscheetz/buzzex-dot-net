// -----------------------------------------------------------------------------
// <copyright file="DepthResponse" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/9/2019 11:18:30 AM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public class DepthResponse
    {
        #region Properties

        public Dictionary<string, Depth> Datas { get; set; }

        #endregion Properties
    }
}