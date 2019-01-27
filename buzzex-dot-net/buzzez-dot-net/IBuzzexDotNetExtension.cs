// -----------------------------------------------------------------------------
// <copyright file="IBuzzexDotNetExtension" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 8:35:12 AM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net
{
    using buzzez_dot_net.Contracts;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public static class IBuzzexDotNetExtension
    {
        public static async Task<PairResponse> GetInfo(this IBuzzexDotNet service)
        {
            return await service.GetInfo(1);
        }

        public static async Task<Dictionary<string, TradingPairBase>> GetAllInfo(this IBuzzexDotNet service)
        {
            var pairDictionary = new Dictionary<string, TradingPairBase>();
            var currentPage = 1;
            var lastPage = false;

            while (!lastPage)
            {
                var result = await service.GetInfo(currentPage);

                foreach (var pair in result.Pairs)
                {
                    foreach (var kvp in pair)
                    {
                        pairDictionary.Add(kvp.Key, kvp.Value);
                    }
                }

                currentPage++;
                if (currentPage >= result.Meta.Pagination.TotalPages)
                    lastPage = true;
            }

            return pairDictionary;
        }

        public static async Task<List<TradingPair>> GetInfoConverted(this IBuzzexDotNet service)
        {
            return await GetInfoConverted(service, 1);
        }

        public static async Task<List<TradingPair>> GetInfoConverted(this IBuzzexDotNet service, int page)
        {
            var result = await service.GetInfo(page);
            
            return ConvertTradingPairs(result.Pairs);
        }

        public static async Task<List<TradingPair>> GetAllInfoConverted(this IBuzzexDotNet service)
        {
            var pairs = await GetAllInfo(service);

            var tradingPairs = new List<TradingPair>();

            foreach (var pair in pairs)
            {
                var tradingPair = new TradingPair(pair.Key, pair.Value);
                tradingPairs.Add(tradingPair);
            }

            return tradingPairs;
        }

        private static List<TradingPair> ConvertTradingPairs(List<Dictionary<string, TradingPairBase>> response)
        {
            var pairList = new List<TradingPair>();

            foreach(var item in response)
            {
                foreach(var kvp in item)
                {
                    var tradingPair = new TradingPair(kvp.Key, kvp.Value);
                    pairList.Add(tradingPair);
                }
            }

            return pairList;
        }

        public static async Task<Ticker> GetTickerConverted(this IBuzzexDotNet service, string pair)
        {
            var tickerBase = await service.GetTicker(pair);

            var tickerList = new List<Ticker>();

            foreach (var ticker in tickerBase)
            {
                foreach (var kvp in ticker)
                {
                    var tick = new Ticker(kvp.Key, kvp.Value);

                    tickerList.Add(tick);
                }
            }

            return tickerList.FirstOrDefault();
        }

        public static async Task<DepthDetail> GetDepthConverted(this IBuzzexDotNet service, string pair)
        {
            var result = await service.GetDepth(pair);
            var asks = result.Asks;
            var bids = result.Bids;

            var asksInfo = new List<DepthInfo>();
            var bidsInfo = new List<DepthInfo>();
            for (var i = 0; i < asks.Count; i++)
            {
                var depthInfo = new DepthInfo
                {
                    Amount = asks[i][0],
                    Price = asks[i][1]
                };
                asksInfo.Add(depthInfo);
            }
            for (var i = 0; i < bids.Count; i++)
            {
                var depthInfo = new DepthInfo
                {
                    Amount = bids[i][0],
                    Price = bids[i][1]
                };
                bidsInfo.Add(depthInfo);
            }

            return new DepthDetail { Asks = asksInfo, Bids = bidsInfo };
        }
    }
}