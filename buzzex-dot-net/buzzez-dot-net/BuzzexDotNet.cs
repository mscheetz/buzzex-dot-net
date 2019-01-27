// -----------------------------------------------------------------------------
// <copyright file="BuzzexDotNet" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 8:31:38 AM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net
{
    #region Usings

    using buzzez_dot_net.Contracts;
    using buzzez_dot_net.Interfaces;
    using buzzez_dot_net.Repository;
    // using RESTApiAccess;
    //using RESTApiAccess.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public class BuzzexDotNet : IBuzzexDotNet
    {
        #region Properties

        private IRESTRepository _rest;
        private string apiKey = string.Empty;
        private string apiSecret = string.Empty;
        private string baseUrl;

        #endregion Properties

        public BuzzexDotNet()
        {
        }

        public BuzzexDotNet(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            LoadRepository();
        }

        public BuzzexDotNet(ApiCredentials apiCredentials)
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
        
        public async Task<PairResponse> GetInfo(int page)
        {
            var endpoint = "v1/info";

            if (page > 1)
            {
                endpoint += $"?page={page}";
            }

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<PairResponse>(url, headers);

            return response;
        }

        public async Task<List<Dictionary<string, TickerBase>>> GetTicker(string pair)
        {
            var endpoint = $"v1/ticker/{pair}";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<List<Dictionary<string, TickerBase>>>(url, headers);

            return response;
        }

        public async Task<Depth> GetDepth(string pair)
        {
            var endpoint = $"v1/depth/{pair}";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<List<DepthResponse>>(url, headers);

            Depth depth = null;

            foreach (var data in response[0].Datas)
            {
                if (depth == null)
                {
                    depth = data.Value;
                    break;
                }
            }

            return depth;
        }

        public async Task<List<Trade>> OnGetTrades(string pair)
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

        public async Task<AccountInfo> GetBalances()
        {
            var endpoint = $"v1/trading/getinfo";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<AccountInfo>(url, headers);

            return response;
        }

        public async Task<bool> PlaceOrder(string pair, TradeType tradeType, decimal price, decimal quantity)
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