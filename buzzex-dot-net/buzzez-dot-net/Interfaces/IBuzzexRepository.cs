// -----------------------------------------------------------------------------
// <copyright file="IBuzzexRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 10:04:59 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Interfaces
{
    using buzzez_dot_net.Contracts;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IBuzzexRepository
    {
        Task<PairResponse> GetInfo();

        Task<PairResponse> GetInfo(int page);

        Task<Dictionary<string, TradingPairBase>> GetAllInfo();

        Task<List<TradingPair>> GetInfoConverted();

        Task<List<TradingPair>> GetInfoConverted(int page);

        Task<List<TradingPair>> GetAllInfoConverted();

        Task<Dictionary<string, TickerBase>> GetTicker(string pair);

        Task<Ticker> GetTickerConverted(string pair);

        Task<Depth> GetDepth(string pair);

        Task<DepthDetail> GetDepthConverted(string pair);

        Task<List<Trade>> GetTrades(string pair);
    }
}