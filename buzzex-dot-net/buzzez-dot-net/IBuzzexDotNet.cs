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
        Task<PairResponse> GetInfo(int page);

        Task<List<Dictionary<string, TickerBase>>> GetTicker(string pair);

        Task<Depth> GetDepth(string pair);

        Task<List<Trade>> OnGetTrades(string pair);

        Task<AccountInfo> GetBalances();

        Task<bool> PlaceOrder(string pair, TradeType tradeType, decimal price, decimal quantity);
    }
}