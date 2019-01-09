// -----------------------------------------------------------------------------
// <copyright file="Order" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:16:31 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings
    #endregion Usings

    public class Order : OrderBase
    {
        #region Properties

        public string OrderId { get; set; }

        #endregion Properties
    }
}