using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Entities;
using WooliesX.Infrastructure.Respositories;
using Xunit;

namespace WooliesX.Infrastructure.Tests
{
    public class ShopperRespositoryTests
    {
        private readonly Mock<IHttpClientHelper> _mockHttpClientHelper;
        private readonly Mock<ILogger<ShopperRespository>> _mockLogger;
        private readonly IShopperRespository _sut;

        public ShopperRespositoryTests()
        {
            _mockLogger = new Mock<ILogger<ShopperRespository>>(MockBehavior.Loose);
            _mockHttpClientHelper = new Mock<IHttpClientHelper>(MockBehavior.Strict);

            var options = new Mock<IOptions<AppSettings>>();
            options.Setup(s => s.Value).Returns(new AppSettings
            {
                BaseUrl = "www.foo.com",
                ShopperHistoryEndPoint = "shopperhistory",
                Token = "Foo.Token"
            });

            _sut = new ShopperRespository(_mockHttpClientHelper.Object, options.Object, _mockLogger.Object);
        }

        [Fact]
        public void Ctor_ShouldThrowExceptionWhenRequiredAppSettingsAreNull()
        {
            var options1 = new Mock<IOptions<AppSettings>>();
            options1.Setup(s => s.Value).Returns(new AppSettings { BaseUrl = null });

            Assert.Throws<ArgumentNullException>(() => new ShopperRespository(_mockHttpClientHelper.Object, options1.Object, _mockLogger.Object));

            var options2 = new Mock<IOptions<AppSettings>>();
            options2.Setup(s => s.Value).Returns(new AppSettings { BaseUrl = "blah", ShopperHistoryEndPoint = null });

            Assert.Throws<ArgumentNullException>(() => new ShopperRespository(_mockHttpClientHelper.Object, options2.Object, _mockLogger.Object));

            var options3 = new Mock<IOptions<AppSettings>>();
            options3.Setup(s => s.Value).Returns(new AppSettings { BaseUrl = "blah", ShopperHistoryEndPoint = "foo", Token = null });

            Assert.Throws<ArgumentNullException>(() => new ShopperRespository(_mockHttpClientHelper.Object, options3.Object, _mockLogger.Object));
        }

        [Fact]
        public void GetProducts_ShouldReturnProducts()
        {
            var expected = new List<ShopperHistoryEntity>
            {
                new ShopperHistoryEntity { CustomerId = 1, Products = It.IsAny<List<ProductEntity>>() },
                new ShopperHistoryEntity { CustomerId = 2, Products = It.IsAny<List<ProductEntity>>() },
                new ShopperHistoryEntity { CustomerId = 3, Products = It.IsAny<List<ProductEntity>>() }
            };

            _mockHttpClientHelper.Setup(s => s.GetAsync<IEnumerable<ShopperHistoryEntity>>(It.IsAny<string>())).ReturnsAsync(expected);

            var result = _sut.GetShopperHistory().Result;

            Assert.Equal(expected, result);
        }
    }
}
