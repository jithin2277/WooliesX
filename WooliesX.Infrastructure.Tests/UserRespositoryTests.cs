using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Entities;
using WooliesX.Infrastructure.Respositories;
using Xunit;

namespace WooliesX.Infrastructure.Tests
{
    public class UserRespositoryTests
    {
        private readonly Mock<ILogger<UserRespository>> _mockLogger;
        private readonly IUserRespository _sut;

        public UserRespositoryTests()
        {
            _mockLogger = new Mock<ILogger<UserRespository>>(MockBehavior.Loose);
            var options = new Mock<IOptions<AppSettings>>();
            options.Setup(s => s.Value).Returns(new AppSettings { Token = "Foo.Token" });

            _sut = new UserRespository(options.Object, _mockLogger.Object);
        }

        [Fact]
        public void Ctor_ShouldThrowExceptionWhenTokenIsNull()
        {
            var options = new Mock<IOptions<AppSettings>>();
            options.Setup(s => s.Value).Returns(new AppSettings { Token = null });

            Assert.Throws<ArgumentNullException>(() => new UserRespository(options.Object, _mockLogger.Object));
        }

        [Fact]
        public void GetUserDetails_ShouldReturnUserDetails()
        {
            var name = "Foo";
            var expected = new UserEntity { Name = name, Token = "Foo.Token" };

            var result = _sut.GetUserDetails(name).Result;

            Assert.Equal(expected.Token, result.Token);
            Assert.Equal(expected.Name, result.Name);
        }
    }
}
