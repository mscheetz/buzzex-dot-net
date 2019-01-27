// -----------------------------------------------------------------------------
// <copyright file="IBuzzexDotNet" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 8:32:10 AM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net
{
    using buzzez_dot_net.Contracts;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IBuzzexDotNet
    {
        /// <summary>
        /// Get information on trading pairs
        /// </summary>
        /// <param name="page">Page number</param>
        /// <returns>Paged response of trading pairs</returns>
        Task<PairResponse> GetInfo(int page);

        /// <summary>
        /// Get Ticker information for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Ticker information on trading pair</returns>
        Task<List<Dictionary<string, TickerBase>>> GetTicker(string pair);

        /// <summary>
        /// Get order book depth for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Order book depth</returns>
        Task<Dictionary<string, Depth>> GetDepth(string pair);

        /// <summary>
        /// Get recent trades for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Collection of recent trades</returns>
        Task<List<Trade>> GetTrades(string pair);

        /// <summary>
        /// Provides balances for current account
        /// </summary>
        /// <returns>AccountInfo object</returns>
        Task<AccountInfo> GetBalances();

        /// <summary>
        /// Place a new order
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="side">Trade side</param>
        /// <param name="price">Price of order</param>
        /// <param name="quantity">Quantity to trade</param>
        /// <returns>TBD</returns>
        Task<bool> PlaceOrder(string pair, Side side, decimal price, decimal quantity);
    }
}