// -----------------------------------------------------------------------------
// <copyright file="TradingPair" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/8/2019 7:52:43 PM" />
// -----------------------------------------------------------------------------

namespace buzzez_dot_net.Contracts
{
    #region Usings
    #endregion Usings

    public class TradingPair : TradingPairBase
    {
        #region Properties

        public string Pair { get; set; }

        #endregion Properties

        public TradingPair()
        {
        }

        public TradingPair(string tradingPair, TradingPairBase pairBase)
        {
            Pair = tradingPair;
            SetBaseProperties(pairBase);
        }

        public TradingPair(TradingPairBase pairBase)
        {
            SetBaseProperties(pairBase);
        }

        private void SetBaseProperties(TradingPairBase pairBase)
        {
            base.DecimalPlaces = pairBase.DecimalPlaces;
            base.Fee = pairBase.Fee;
            base.FeeBuyer = pairBase.FeeBuyer;
            base.FeeSeller = pairBase.FeeSeller;
            base.Hidden = pairBase.Hidden;
            base.MaxPrice = pairBase.MaxPrice;
            base.MinAmount = pairBase.MinAmount;
            base.MinPrice = pairBase.MinPrice;
            base.MinTotal = pairBase.MinTotal;
        }
    }
}