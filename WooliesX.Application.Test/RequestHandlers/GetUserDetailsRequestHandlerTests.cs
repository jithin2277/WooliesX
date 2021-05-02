using AutoMapper;
using Moq;
using System;
using System.Threading;
using WooliesX.Application.Common.Mappings;
using WooliesX.Application.Core;
using WooliesX.Application.UserDetails;
using WooliesX.Domain.Entities;
using Xunit;

namespace WooliesX.Application.Test
{
    public class GetUserDetailsRequestHandlerTests : IDisposable
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly IMapper _mockMapper;
        private readonly GetUserDetailsRequestHandler _sut;

        public GetUserDetailsRequestHandlerTests()
        {
            _mockUserService = new Mock<IUserService>(MockBehavior.Strict);
            _mockMapper = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            }).CreateMapper();

            _sut = new GetUserDetailsRequestHandler(_mockUserService.Object, _mockMapper);
        }

        public void Dispose()
        {
            _mockUserService.VerifyAll();
        }

        [Fact]
        public void Handle_ShouldGetUserDetails()
        {
            var name = "Foo Bar";
            var token = "Foo.Token";

            var expected = new UserDto
            {
                Name = name,
                Token = token
            };
            var request = new GetUserDetailsRequest
            {
                Name = name
            };

            _mockUserService.Setup(s => s.GetUserDetails(name)).ReturnsAsync(new UserEntity
            {
                Name = name,
                Token = token
            });

            var result = _sut.Handle(request, It.IsAny<CancellationToken>()).Result;

            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.Token, result.Token);
        }
    }
}
