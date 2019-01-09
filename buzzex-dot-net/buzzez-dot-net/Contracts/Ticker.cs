// -----------------------------------------------------------------------------
// <copyright file="Ticker" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 8:06:12 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class Ticker : TickerBase
    {
        #region Properties

        public string Pair { get; set; }
        
        #endregion Properties

        public Ticker()
        { }

        public Ticker(string pair, TickerBase tickerBase)
        {
            Pair = pair;
            SetBaseProperties(tickerBase);
        }

        public Ticker(TickerBase tickerBase)
        {
            SetBaseProperties(tickerBase);
        }

        private void SetBaseProperties(TickerBase tickerBase)
        {
            base.BaseVolume = tickerBase.BaseVolume;
            base.High = tickerBase.High;
            base.High24h = tickerBase.High24h;
            base.IsFrozen = tickerBase.IsFrozen;
            base.Last = tickerBase.Last;
            base.Low = tickerBase.Low;
            base.Low24h = tickerBase.Low24h;
            base.PairId = tickerBase.PairId;
            base.PercentChange = tickerBase.PercentChange;
            base.Price24h = tickerBase.Price24h;
            base.QuoteVolume = tickerBase.QuoteVolume;
            base.Updated = tickerBase.Updated;
        }
    }
}