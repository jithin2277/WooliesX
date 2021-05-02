using Moq;
using System;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Application.Core;
using WooliesX.Domain.Entities;
using Xunit;

namespace WooliesX.Application.Test
{
    public class UserServiceTests : IDisposable
    {
        private readonly Mock<IUserRespository> _mockUserRespository;
        private readonly IUserService _sut;

        public UserServiceTests()
        {
            _mockUserRespository = new Mock<IUserRespository>(MockBehavior.Strict);
            _sut = new UserService(_mockUserRespository.Object);
        }

        public void Dispose()
        {
            _mockUserRespository.VerifyAll();
        }

        [Fact]
        public void GetUserDetails_ShouldGetUserDetailsForName()
        {
            var name = "Foo Bar";
            var token = "Foo.Token";

            var expected = new UserEntity
            {
                Name = name,
                Token = token
            };

            _mockUserRespository.Setup(s => s.GetUserDetails(name)).ReturnsAsync(expected);

            var result = _sut.GetUserDetails(name).Result;

            Assert.Equal(expected, result);
        }
    }
}
