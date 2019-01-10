// -----------------------------------------------------------------------------
// <copyright file="BuzzexRepositoryTests" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/9/2019 11:26:30 AM" />
// -----------------------------------------------------------------------------

namespace buzzex_dot_net.Tests
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    #endregion Usings

    public class BuzzexRepositoryTests : IDisposable
    {
        #region Properties
        #endregion Properties

        public BuzzexRepositoryTests()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #region Tests

        [Fact]
        public void ListListTest()
        {
            var expected = new List<List<decimal>>();
            var inner1 = new List<decimal>
            {
                0,
                148807.70988252M
            };
            var inner2 = new List<decimal>
            {
                    0.1M,
                    40
            };
            expected.Add(inner1);
            expected.Add(inner2);

            Assert.True(expected.Count > 0);
        }

        #endregion Tests
    }
}