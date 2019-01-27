// -----------------------------------------------------------------------------
// <copyright file="IBuzzexDotNetExtension" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 8:35:12 AM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net
{
    #region Usings

    using buzzez_dot_net.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion Usings

    public static class IBuzzexDotNetExtension
    {
        #region Public Api Extensions

        /// <summary>
        /// Get information for trading pairs
        /// 1st page only
        /// </summary>
        /// <param name="service">BuzzexDotNet service</param>
        /// <returns>All trading pair information</returns>
        public static async Task<PairResponse> GetInfo(this IBuzzexDotNet service)
        {
            return await service.GetInfo(1);
        }

        /// <summary>
        /// Get information for all trading pairs
        /// </summary>
        /// <param name="service">BuzzexDotNet service</param>
        /// <returns>All trading pair information</returns>
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

        /// <summary>
        /// Get converted information for trading pairs
        /// 1st page only
        /// </summary>
        /// <param name="service">BuzzexDotNet service</param>
        /// <returns>Collection of TradingPair objects</returns>
        public static async Task<List<TradingPair>> GetInfoConverted(this IBuzzexDotNet service)
        {
            return await GetInfoConverted(service, 1);
        }

        /// <summary>
        /// Get converted information for trading pairs
        /// </summary>
        /// <param name="service">BuzzexDotNet service</param>
        /// <param name="page">Page number</param>
        /// <returns>Collection of TradingPair objects</returns>
        public static async Task<List<TradingPair>> GetInfoConverted(this IBuzzexDotNet service, int page)
        {
            var result = await service.GetInfo(page);
            
            return ConvertTradingPairs(result.Pairs);
        }

        /// <summary>
        /// Get converted information for all trading pairs
        /// </summary>
        /// <param name="service">BuzzexDotNet service</param>
        /// <returns>Collection of TradingPair objects</returns>
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

        /// <summary>
        /// Get converted ticker
        /// </summary>
        /// <param name="service">BuzzexDotNet service</param>
        /// <param name="pair">Trading pair</param>
        /// <returns>Converted Ticker object</returns>
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

        /// <summary>
        /// Get converted Depth
        /// </summary>
        /// <param name="service">BuzzexDotNet service</param>
        /// <param name="pair">Trading pair</param>
        /// <returns>Converted DepthDetail object</returns>
        public static async Task<DepthDetail> GetDepthConverted(this IBuzzexDotNet service, string pair)
        {
            var result = await service.GetDepth(pair);            
            List<List<decimal>> asks = null;
            List<List<decimal>> bids = null;
            foreach (var kvp in result)
            {
                asks = kvp.Value.Asks;
                bids = kvp.Value.Bids;
            }

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

            return new DepthDetail { Pair = pair, Asks = asksInfo, Bids = bidsInfo };
        }

        #endregion Public Api Extensions

        #region Trading Api Extensions
        
        /// <summary>
        /// Place a Limit Order
        /// </summary>
        /// <param name="service">BuzzexDotNet service</param>
        /// <param name="pair">Trading pair</param>
        /// <param name="side">Trade side</param>
        /// <param name="price">Price of order</param>
        /// <param name="quantity">Quantity to trade</param>
        public static async Task<bool> LimitOrder(this IBuzzexDotNet service, string pair, Side side, decimal price, decimal quantity)
        {
            var response = await service.PlaceOrder(pair, side, price, quantity);

            return response;
        }

        /// <summary>
        /// Place a Market Order
        /// </summary>
        /// <param name="service">BuzzexDotNet service</param>
        /// <param name="pair">Trading pair</param>
        /// <param name="side">Trade side</param>
        /// <param name="quantity">Quantity to trade</param>
        /// <returns></returns>
        public static async Task<bool> MarketOrder(this IBuzzexDotNet service, string pair, Side side, decimal quantity)
        {
            var ticker = await service.GetTickerConverted(pair);

            var response = await service.PlaceOrder(pair, side, ticker.Last, quantity);

            return response;
        }

        #endregion Trading Api Extensions

        #region Helpers
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static List<TradingPair> ConvertTradingPairs(List<Dictionary<string, TradingPairBase>> response)
        {
            var pairList = new List<TradingPair>();

            foreach (var item in response)
            {
                foreach (var kvp in item)
                {
                    var tradingPair = new TradingPair(kvp.Key, kvp.Value);
                    pairList.Add(tradingPair);
                }
            }

            return pairList;
        }

        #endregion Helpers
    }
}