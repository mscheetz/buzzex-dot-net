// -----------------------------------------------------------------------------
// <copyright file="BuzzexRepositoryTests" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/9/2019 11:26:30 AM" />
// -----------------------------------------------------------------------------

namespace buzzex_dot_net.Tests
{
    using buzzez_dot_net.Contracts;
    using buzzez_dot_net.Interfaces;
    using buzzez_dot_net.Repository;
    using FileRepository;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    #endregion Usings

    public class BuzzexRepositoryTests : IDisposable
    {
        #region Properties

        private IBuzzexRepository _repo;
        private string configPath = "config.json";
        private ApiCredentials _apiCreds;

        #endregion Properties

        public BuzzexRepositoryTests()
        {
            IFileRepository _fileRepo = new FileRepository();
            if (_fileRepo.FileExists(configPath))
            {
                _apiCreds = _fileRepo.GetDataFromFile<ApiCredentials>(configPath);
            }
            if (_apiCreds != null || !string.IsNullOrEmpty(_apiCreds.ApiKey))
            {
                _repo = new BuzzexRepository(_apiCreds);
            }
            else
            {
                _repo = new BuzzexRepository();
            }
        }

        public void Dispose()
        {
        }

        #region Tests

        [Fact]
        public void GetInfo_Test()
        {
            var info = _repo.GetInfo().Result;

            Assert.NotNull(info);
        }

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