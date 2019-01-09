// -----------------------------------------------------------------------------
// <copyright file="BuzzexRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 10:04:51 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Repository
{
    using buzzez_dot_net.Contracts;
    #region Usings

    using buzzez_dot_net.Interfaces;
    using RESTApiAccess;
    using RESTApiAccess.Interface;
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

                foreach(var pair in result.Pairs)
                {
                    pairDictionary.Add(pair.Key, pair.Value);
                }
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

            foreach (var pair in result.Pairs)
            {
                var tradingPair = new TradingPair(pair.Key, pair.Value);
                tradingPairs.Add(tradingPair);
            }

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

        public async Task<Depth<DepthDetail>> GetDepth(string pair)
        {
            return await OnGetDepth<DepthDetail>(pair);
        }

        public async Task<Depth<DepthInfo>> GetDepthConverted(string pair)
        {
            var result = await OnGetDepth<DepthDetail>(pair);
            var asks = result.DepthDetail["asks"];
            var bids = result.DepthDetail["bids"];


        }

        private async Task<Depth<T>> OnGetDepth<T>(string pair)
        {
            var endpoint = $"v1/depth/{pair}";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<Depth<T>>(url, headers);

            return response;
        }

        #endregion Public Api

        #region Private Api
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