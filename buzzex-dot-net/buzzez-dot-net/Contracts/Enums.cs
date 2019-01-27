// -----------------------------------------------------------------------------
// <copyright file="Enums" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:12:47 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public enum TradeType
    {
        ask,
        bid
    }

    public enum Side
    {
        buy,
        sell
    }
}