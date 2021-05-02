using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Entities.Trolley;
using WooliesX.Infrastructure.Respositories;
using Xunit;

namespace WooliesX.Infrastructure.Tests
{
    public class TrolleyRepositoryTests
    {
        private readonly Mock<IHttpClientHelper> _mockHttpClientHelper;
        private readonly Mock<ILogger<TrolleyRepository>> _mockLogger;
        private readonly ITrolleyRepository _sut;

        public TrolleyRepositoryTests()
        {
            _mockLogger = new Mock<ILogger<TrolleyRepository>>(MockBehavior.Loose);
            _mockHttpClientHelper = new Mock<IHttpClientHelper>(MockBehavior.Strict);

            var options = new Mock<IOptions<AppSettings>>();
            options.Setup(s => s.Value).Returns(new AppSettings
            {
                BaseUrl = "www.foo.com",
                TrolleyCalculatorEndPoint = "trolley",
                Token = "Foo.Token"
            });

            _sut = new TrolleyRepository(_mockHttpClientHelper.Object, options.Object, _mockLogger.Object);
        }

        [Fact]
        public void Ctor_ShouldThrowExceptionWhenRequiredAppSettingsAreNull()
        {
            var options1 = new Mock<IOptions<AppSettings>>();
            options1.Setup(s => s.Value).Returns(new AppSettings { BaseUrl = null });

            Assert.Throws<ArgumentNullException>(() => new TrolleyRepository(_mockHttpClientHelper.Object, options1.Object, _mockLogger.Object));

            var options2 = new Mock<IOptions<AppSettings>>();
            options2.Setup(s => s.Value).Returns(new AppSettings { BaseUrl = "blah", TrolleyCalculatorEndPoint = null });

            Assert.Throws<ArgumentNullException>(() => new TrolleyRepository(_mockHttpClientHelper.Object, options2.Object, _mockLogger.Object));

            var options3 = new Mock<IOptions<AppSettings>>();
            options3.Setup(s => s.Value).Returns(new AppSettings { BaseUrl = "blah", TrolleyCalculatorEndPoint = "foo", Token = null });

            Assert.Throws<ArgumentNullException>(() => new TrolleyRepository(_mockHttpClientHelper.Object, options3.Object, _mockLogger.Object));
        }

        [Fact]
        public void GetTrolleyTotal_ShouldReturnTrolleyTotal()
        {
            var expected = "123";

            _mockHttpClientHelper.Setup(s => s.PostAsync<TrolleyEntity, string>(It.IsAny<string>(), It.IsAny<TrolleyEntity>())).ReturnsAsync(expected);

            var result = _sut.GetTrolleyTotal(It.IsAny<TrolleyEntity>()).Result;

            Assert.Equal(expected, result);
        }
    }
}
