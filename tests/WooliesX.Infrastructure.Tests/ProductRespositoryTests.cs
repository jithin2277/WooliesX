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
    public class ProductRespositoryTests
    {
        private readonly Mock<IHttpClientHelper> _mockHttpClientHelper;
        private readonly Mock<ILogger<ProductRespository>> _mockLogger;
        private readonly IProductRespository _sut;

        public ProductRespositoryTests()
        {
            _mockLogger = new Mock<ILogger<ProductRespository>>(MockBehavior.Loose);
            _mockHttpClientHelper = new Mock<IHttpClientHelper>(MockBehavior.Strict);

            var options = new Mock<IOptions<AppSettings>>();
            options.Setup(s => s.Value).Returns(new AppSettings
            {
                BaseUrl = "www.foo.com",
                ProductsEndPoint = "products",
                Token = "Foo.Token"
            });

            _sut = new ProductRespository(_mockHttpClientHelper.Object, options.Object, _mockLogger.Object);
        }

        [Fact]
        public void Ctor_ShouldThrowExceptionWhenRequiredAppSettingsAreNull()
        {
            var options1 = new Mock<IOptions<AppSettings>>();
            options1.Setup(s => s.Value).Returns(new AppSettings { BaseUrl = null });

            Assert.Throws<ArgumentNullException>(() => new ProductRespository(_mockHttpClientHelper.Object, options1.Object, _mockLogger.Object));

            var options2 = new Mock<IOptions<AppSettings>>();
            options2.Setup(s => s.Value).Returns(new AppSettings { BaseUrl = "blah", ProductsEndPoint = null });

            Assert.Throws<ArgumentNullException>(() => new ProductRespository(_mockHttpClientHelper.Object, options2.Object, _mockLogger.Object));

            var options3 = new Mock<IOptions<AppSettings>>();
            options3.Setup(s => s.Value).Returns(new AppSettings { BaseUrl = "blah", ProductsEndPoint = "foo", Token = null });

            Assert.Throws<ArgumentNullException>(() => new ProductRespository(_mockHttpClientHelper.Object, options3.Object, _mockLogger.Object));
        }

        [Fact]
        public void GetProducts_ShouldReturnProducts()
        {
            var expected = new List<ProductEntity>
            {
                new ProductEntity { Name = "Foo1", Price = 1, Quantity = 1 },
                new ProductEntity { Name = "Foo2", Price = 2, Quantity = 2 },
                new ProductEntity { Name = "Foo3", Price = 3, Quantity = 3 }
            };

            _mockHttpClientHelper.Setup(s => s.GetAsync<IEnumerable<ProductEntity>>(It.IsAny<string>())).ReturnsAsync(expected);

            var result = _sut.GetProducts().Result;

            Assert.Equal(expected, result);
        }
    }
}
