// -----------------------------------------------------------------------------
// <copyright file="Depth" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:12:11 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using System.Collections.Generic;

    #endregion Usings

    public class Depth<T>
    {
        #region Properties

        public Dictionary<string, T> DepthDetail { get; set; }

        #endregion Properties
    }
}