using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Application.Core;
using WooliesX.Domain.Entities;
using WooliesX.Domain.Enums;
using Xunit;

namespace WooliesX.Application.Test
{
    public class ProductsServiceTests : IDisposable
    {
        private readonly Mock<IProductRespository> _mockProductRespository;
        private readonly Mock<IShopperRespository> _mockShopperRespository;
        private readonly IProductsService _sut;

        private readonly IEnumerable<ProductEntity> _products;

        public ProductsServiceTests()
        {
            _mockProductRespository = new Mock<IProductRespository>(MockBehavior.Strict);
            _mockShopperRespository = new Mock<IShopperRespository>(MockBehavior.Strict);

            _sut = new ProductsService(_mockProductRespository.Object, _mockShopperRespository.Object);

            _products = new List<ProductEntity>
            {
                new ProductEntity { Name = "Foo 1", Price = 1, Quantity = 1 },
                new ProductEntity { Name = "Foo 2", Price = 2, Quantity = 2 },
                new ProductEntity { Name = "Foo 3", Price = 3, Quantity = 3 }
            };
        }

        public void Dispose()
        {
            _mockProductRespository.VerifyAll();
            _mockShopperRespository.VerifyAll();
        }

        [Fact]
        public void GetProducts_ShouldGetAllProductsWhenSortOptionIsNull()
        {
            _mockProductRespository.Setup(s => s.GetProducts()).ReturnsAsync(_products);

            var result = _sut.GetProducts(null).Result;

            Assert.True(result.Count() > 0);
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.Equal(_products.ElementAt(i).Name, result.ElementAt(i).Name);
                Assert.Equal(_products.ElementAt(i).Price, result.ElementAt(i).Price);
                Assert.Equal(_products.ElementAt(i).Quantity, result.ElementAt(i).Quantity);
            }
        }

        [Theory]
        [InlineData(SortOption.Ascending)]
        [InlineData(SortOption.Descending)]
        [InlineData(SortOption.High)]
        [InlineData(SortOption.Low)]
        [InlineData(SortOption.Recommended)]
        public void GetProducts_ShouldGetSortedProductsWhenSortOptionIsNotNullAndNotRecommended(SortOption sortOption)
        {
            _mockProductRespository.Setup(s => s.GetProducts()).ReturnsAsync(_products);

            var sorted = _products;
            switch (sortOption)
            {
                case SortOption.Low:
                    sorted = _products.OrderBy(o => o.Price);
                    break;
                case SortOption.High:
                    sorted = _products.OrderByDescending(o => o.Price);
                    break;
                case SortOption.Ascending:
                    sorted = _products.OrderBy(o => o.Name);
                    break;
                case SortOption.Descending:
                    sorted = _products.OrderByDescending(o => o.Name);
                    break;
                case SortOption.Recommended:
                    sorted = SortByRecommended(_products);
                    break;
                default:
                    break;
            }

            var result = _sut.GetProducts(sortOption).Result;

            Assert.True(result.Count() > 0);
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.Equal(sorted.ElementAt(i).Name, result.ElementAt(i).Name);
                Assert.Equal(sorted.ElementAt(i).Price, result.ElementAt(i).Price);
                Assert.Equal(sorted.ElementAt(i).Quantity, result.ElementAt(i).Quantity);
            }
        }

        private IEnumerable<ProductEntity> SortByRecommended(IEnumerable<ProductEntity> products)
        {
            var shopperHistory = new List<ShopperHistoryEntity>
            {
                new ShopperHistoryEntity {
                    CustomerId = 1,
                    Products = new List<ProductEntity>
                    {
                        new ProductEntity { Name = "1", Price = 1, Quantity = 11 },
                        new ProductEntity { Name = "2", Price = 10, Quantity = 12 },
                        new ProductEntity { Name = "3", Price = 20, Quantity = 13 }
                    }
                }
            };
            _mockShopperRespository.Setup(s => s.GetShopperHistory()).ReturnsAsync(shopperHistory);

            return shopperHistory
                .SelectMany(s => s.Products)
                .Concat(products)
                .GroupBy(g => g.Name)
                .Select(s => new ProductEntity
                {
                    Name = s.Key,
                    Price = s.Where(w => w.Name == s.Key).First().Price,
                    Quantity = s.Sum(u => u.Quantity)
                })
                .OrderByDescending(o => o.Quantity);
        }
    }
}
