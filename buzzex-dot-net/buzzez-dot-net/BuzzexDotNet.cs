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
    //using RESTApiAccess;
    //using RESTApiAccess.Interface;
    using System.Collections.Generic;
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

        /// <summary>
        /// Constructor
        /// </summary>
        public BuzzexDotNet()
        {
            LoadRepository();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">Api key value</param>
        /// <param name="apiSecret">Api secret value</param>
        public BuzzexDotNet(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            LoadRepository();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiCredentials">ApiCredentials object</param>
        public BuzzexDotNet(ApiCredentials apiCredentials)
        {
            this.apiKey = apiCredentials.ApiKey;
            this.apiSecret = apiCredentials.ApiSecret;
            LoadRepository();
        }

        /// <summary>
        /// Load the repository
        /// </summary>
        private void LoadRepository()
        {
            baseUrl = "https://api.buzzex.io/api/";
            _rest = new RESTRepository();
        }

        #region Public Api
        
        /// <summary>
        /// Get information on trading pairs
        /// </summary>
        /// <param name="page">Page number</param>
        /// <returns>Paged response of trading pairs</returns>
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

        /// <summary>
        /// Get Ticker information for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Ticker information on trading pair</returns>
        public async Task<List<Dictionary<string, TickerBase>>> GetTicker(string pair)
        {
            var endpoint = $"v1/ticker/{pair}";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<List<Dictionary<string, TickerBase>>>(url, headers);

            return response;
        }

        /// <summary>
        /// Get order book depth for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Order book depth</returns>
        public async Task<Dictionary<string, Depth>> GetDepth(string pair)
        {
            var endpoint = $"v1/depth/{pair}";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<List<Dictionary<string, Depth>>>(url, headers);

            return response[0];
        }

        /// <summary>
        /// Get recent trades for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Collection of recent trades</returns>
        public async Task<List<Trade>> GetTrades(string pair)
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

        /// <summary>
        /// Provides balances for current account
        /// </summary>
        /// <returns>AccountInfo object</returns>
        public async Task<AccountInfo> GetBalances()
        {
            var endpoint = $"v1/trading/getinfo";

            var url = baseUrl + endpoint;
            var headers = GetHeaders();

            var response = await _rest.GetApiStream<AccountInfo>(url, headers);

            return response;
        }

        /// <summary>
        /// Place a new order
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="side">Trade side</param>
        /// <param name="price">Price of order</param>
        /// <param name="quantity">Quantity to trade</param>
        /// <returns>TBD</returns>
        public async Task<bool> PlaceOrder(string pair, Side side, decimal price, decimal quantity)
        {
            var endpoint = $"v1/trading/trade";

            var url = baseUrl + endpoint;

            var data = new Dictionary<string, object>();
            data.Add("pair", pair);
            data.Add("type", side.ToString());
            data.Add("rate", price);
            data.Add("amount", quantity);

            var headers = GetHeaders();

            var response = await _rest.PostApi<bool, Dictionary<string, object>>(url, data, headers);

            return response;
        }

        /// <summary>
        /// Get open orders for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Collection of open orders</returns>
        public async Task<OpenOrderResponse> GetOpenOrders(string pair)
        {
            var endpoint = $"v1/trading/active-orders/{pair}";

            var url = baseUrl + endpoint;

            var headers = GetHeaders();

            var response = await _rest.GetApiStream<OpenOrderResponse>(url, headers);

            return response;
        }

        /// <summary>
        /// Get information about an order
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns>Order details</returns>
        public async Task<OrderResponse> GetOrder(string orderId)
        {
            var endpoint = $"v1/trading/order-info/{orderId}";

            var url = baseUrl + endpoint;

            var headers = GetHeaders();

            var response = await _rest.GetApiStream<OrderResponse>(url, headers);

            return response;
        }

        /// <summary>
        /// Get completed orders
        /// </summary>
        /// <returns>OrderResponse object</returns>
        public async Task<OrderResponse> GetOrders()
        {
            var endpoint = $"v1/trading/trade/history";

            var queryString = new Dictionary<string, string>();

            var url = baseUrl + endpoint;

            var headers = GetHeaders();

            var response = await _rest.GetApiStream<OrderResponse>(url, headers);

            return response;
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns>Canceled order details</returns>
        public async Task<CancelOrderResponse> CancelOrder(string orderId)
        {
            var endpoint = $"v1/trading/cancel-order/{orderId}";

            var url = baseUrl + endpoint;

            var headers = GetHeaders();

            var response = await _rest.GetApiStream<CancelOrderResponse>(url, headers);

            return response;
        }

        /// <summary>
        /// Get a deposit address for a currency
        /// </summary>
        /// <param name="symbol">Currency symbol</param>
        /// <param name="newAddress">Request a new address?</param>
        /// <returns>String of deposit address</returns>
        public async Task<string> GetDepositAddress(string symbol, bool newAddress)
        {
            var endpoint = $"v1/trading/get-deposit-address/{symbol}/{newAddress}";

            var url = baseUrl + endpoint;

            var headers = GetHeaders();

            var response = await _rest.GetApi<DepositAddressResponse>(url, headers);

            return response.Address;
        }

        /// <summary>
        /// Withdraw funds
        /// </summary>
        /// <param name="symbol">Currency symbol</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="address">Recipient address</param>
        /// <returns>TBD</returns>
        public async Task<bool> Withdrawal(string symbol, decimal quantity, string address)
        {
            var endpoint = $"v1/trading/trade";

            var url = baseUrl + endpoint;

            var data = new Dictionary<string, object>();
            data.Add("coinName", symbol);
            data.Add("amount", quantity);
            data.Add("address", address);

            var headers = GetHeaders();

            var response = await _rest.PostApi<bool, Dictionary<string, object>>(url, data, headers);

            return response;
        }

        #endregion Private Api

        #region Private Methods

        /// <summary>
        /// Get headers for requests
        /// </summary>
        /// <returns>Dictionary of headers</returns>
        private Dictionary<string, string> GetHeaders()
        {
            var headers = new Dictionary<string, string>();
            headers.Add("accept", "application/json");

            return headers;
        }

        #endregion Private Methods
    }
}