// -----------------------------------------------------------------------------
// <copyright file="BuzzexRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 10:04:51 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Repository
{
    #region Usings

    using buzzez_dot_net.Contracts;
    using buzzez_dot_net.Interfaces;
   // using RESTApiAccess;
    //using RESTApiAccess.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public class BuzzexRepository : IBuzzexRepository
    {
        #region Properties

        private IRESTRepository _rest;
        private string apiKey = string.Empty;
        private string apiSecret = string.Empty;
        private string baseUrl;

        #endregion Properties

        public BuzzexRepository()
        {
        }

        public BuzzexRepository(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            LoadRepository();
        }

        public BuzzexRepository(ApiCredentials apiCredentials)
        {
            this.apiKey = apiCredentials.ApiKey;
            this.apiSecret = apiCredentials.ApiSecret;
            LoadRepository();
        }

        private void LoadRepository()
        {
            baseUrl = "https://api.buzzex.io/api/";
            _rest = new RESTRepository();
        }

        #region Public Api

        public async Task<PairResponse> GetInfo()
        {
            return await OnGetInfo(1);
        }

        public async Task<PairResponse> GetInfo(int page)
        {
            return await OnGetInfo(page);
        }

        public async Task<Dictionary<string, TradingPairBase>> GetAllInfo()
        {
            var pairDictionary = new Dictionary<string, TradingPairBase>();
            var currentPage = 1;
            var lastPage = false;

            while(!lastPage)
            {
                var result = await OnGetInfo(currentPage);

                //foreach(var pair in result.Pairs)
                //{
                //    pairDictionary.Add(pair.Key, pair.Value);
                //}
                currentPage++;
                if (currentPage >= result.Meta.Pagination.TotalPages)
                    lastPage = true;
            }

            return pairDictionary;
        }

        public async Task<List<TradingPair>> GetInfoConverted()
        {
            return await GetInfoConverted(1);
        }

        public async Task<List<TradingPair>> GetInfoConverted(int page)
        {
            var result = await OnGetInfo(page);

            var tradingPairs = new List<TradingPair>();

            //foreach (var pair in result.Pairs)
            //{
            //    var tradingPair = new TradingPair(pair.Key, pair.Value);
            //    tradingPairs.Add(tradingPair);
            //}

            return tradingPairs;
        }

        public async Task<List<TradingPair>> GetAllInfoConverted()
        {
            var pairs = await GetAllInfo();

            var tradingPairs = new List<TradingPair>();

            foreach (var pair in pairs)
            {
                var tradingPair = new TradingPair(pair.Key, pair.Value);
                tradingPairs.Add(tradingPair);
            }

            return tradingPairs;
        }

        private async Task<PairResponse> OnGetInfo(int page)
        {
            var endpoint = "v1/info";

            if(page > 1)
            {
                endpoint += $"?page={page}";
            }

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<PairResponse>(url, headers);

            return response;
        }

        public async Task<Dictionary<string, TickerBase>> GetTicker(string pair)
        {
            return await OnGetTicker(pair);
        }

        public async Task<Ticker> GetTickerConverted(string pair)
        {
            var tickerBase = await OnGetTicker(pair);

            var tickerList = new List<Ticker>();

            foreach(var ticker in tickerBase)
            {
                var tick = new Ticker(ticker.Key, ticker.Value);

                tickerList.Add(tick);
            }

            return tickerList.FirstOrDefault();
        }

        private async Task<Dictionary<string, TickerBase>> OnGetTicker(string pair)
        {
            var endpoint = $"v1/ticker/{pair}";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<Dictionary<string, TickerBase>>(url, headers);

            return response;
        }

        public async Task<Depth> GetDepth(string pair)
        {
            return await OnGetDepth(pair);
        }

        public async Task<DepthDetail> GetDepthConverted(string pair)
        {
            var result = await OnGetDepth(pair);
            var asks = result.Asks;
            var bids = result.Bids;

            var asksInfo = new List<DepthInfo>();
            var bidsInfo = new List<DepthInfo>();
            for(var i = 0; i< asks.Count; i++)
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

        private async Task<Depth> OnGetDepth(string pair)
        {
            var endpoint = $"v1/depth/{pair}";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<DepthResponse>(url, headers);

            Depth depth = null;

            foreach(var data in response.Datas)
            {
                if(depth == null)
                {
                    depth = data.Value;
                    break;
                }
            }

            return depth;
        }

        public async Task<List<Trade>> GetTrades(string pair)
        {
            return await OnGetTrades(pair);
        }

        private async Task<List<Trade>> OnGetTrades(string pair)
        {
            var endpoint = $"v1/trades/{pair}";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<Dictionary<string, List<Trade>>>(url, headers);

            var trades = new List<Trade>();

            foreach (var data in response)
            {
                if (trades.Count == 0)
                {
                    trades = data.Value;
                    break;
                }
            }

            return trades;
        }

        #endregion Public Api

        #region Private Api
        
        public async Task<AccountInfo> GetBalance()
        {
            return await OnGetBalances();
        }

        private async Task<AccountInfo> OnGetBalances()
        {
            var endpoint = $"v1/trading/getinfo";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<AccountInfo>(url, headers);
            
            return response;
        }

        private async Task<bool> PlaceTrade(string pair, TradeType tradeType, decimal price, decimal quantity)
        {
            return await OnPlaceOrder(pair, tradeType, price, quantity);
        }

        private async Task<bool> OnPlaceOrder(string pair, TradeType tradeType, decimal price, decimal quantity)
        {
            var endpoint = $"v1/trading/trade";

            var url = baseUrl + endpoint;

            var data = new Dictionary<string, object>();
            data.Add("pair", pair);
            data.Add("type", tradeType.ToString());
            data.Add("rate", price);
            data.Add("amount", quantity);

            var headers = GetHeaders();

            var response = await _rest.PostApi<bool, Dictionary<string, object>>(url, data, headers);

            return response;
        }

        #endregion Private Api

        #region Private Methods

        private Dictionary<string, string> GetHeaders()
        {
            var headers = new Dictionary<string, string>();
            headers.Add("accept", "application/json");

            return headers;
        }

        #endregion Private Methods
    }
}