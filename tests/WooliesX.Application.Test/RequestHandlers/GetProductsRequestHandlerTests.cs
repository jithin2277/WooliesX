using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WooliesX.Application.Common.Mappings;
using WooliesX.Application.Core;
using WooliesX.Application.Products;
using WooliesX.Domain.Entities;
using WooliesX.Domain.Enums;
using Xunit;

namespace WooliesX.Application.Test
{
    public class GetProductsRequestHandlerTests : IDisposable
    {
        private readonly Mock<IProductsService> _mockProductsService;
        private readonly IMapper _mockMapper;
        private readonly GetProductsRequestHandler _sut;

        public GetProductsRequestHandlerTests()
        {
            _mockProductsService = new Mock<IProductsService>(MockBehavior.Strict);
            _mockMapper = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            }).CreateMapper();

            _sut = new GetProductsRequestHandler(_mockProductsService.Object, _mockMapper);
        }

        public void Dispose()
        {
            _mockProductsService.VerifyAll();
        }

        [Fact]
        public void Handle_ShouldGetProducts()
        {
            var productEntities = new List<ProductEntity>
            {
                new ProductEntity { Name = "1", Price = 1, Quantity = 1 },
                new ProductEntity { Name = "2", Price = 2, Quantity = 2 },
                new ProductEntity { Name = "3", Price = 3, Quantity = 3 }
            };
            var request = new GetProductsRequest
            {
                SortOption = It.IsAny<SortOption>()
            };

            _mockProductsService.Setup(s => s.GetProducts(request.SortOption)).ReturnsAsync(productEntities);

            var result = _sut.Handle(request, It.IsAny<CancellationToken>()).Result;

            Assert.True(result.Count() == productEntities.Count());
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.Equal(productEntities.ElementAt(i).Name, result.ElementAt(i).Name);
                Assert.Equal(productEntities.ElementAt(i).Price, result.ElementAt(i).Price);
                Assert.Equal(productEntities.ElementAt(i).Quantity, result.ElementAt(i).Quantity);
            }
        }
    }
}
