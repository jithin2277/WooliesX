using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WooliesX.Data.Entities;
using WooliesX.Data.Enums;
using WooliesX.Http;


namespace WooliesX.Data.UnitTests
{
    [TestClass]
    public class ProductsProcessorTests
    {
        private Mock<IHttpClientHelper> _mockHttpClientHelper;
        private Mock<IShopperHistoryProcessor> _mockShopperHistoryProcessor;
        private IProductsProcessor _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpClientHelper = new Mock<IHttpClientHelper>(MockBehavior.Strict);
            _mockShopperHistoryProcessor = new Mock<IShopperHistoryProcessor>(MockBehavior.Strict);
            _sut = new ProductsProcessor(_mockHttpClientHelper.Object, _mockShopperHistoryProcessor.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockHttpClientHelper.VerifyAll();
            _mockShopperHistoryProcessor.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenParametersAreNull_ThrowsException()
        {
            _ = new ProductsProcessor(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenHttpClientHelperIsNull_ThrowsException()
        {
            _ = new ProductsProcessor(null, _mockShopperHistoryProcessor.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenShopperHistoryProcessorIsNull_ThrowsException()
        {
            _ = new ProductsProcessor(_mockHttpClientHelper.Object, null);
        }

        [TestMethod]
        public void GetProducts_ReturnsAllProducts()
        {
            var products = new List<ProductEntity> { 
                new ProductEntity { Name = "A", Price = 1, Quantity = 1 },
                new ProductEntity { Name = "B", Price = 2, Quantity = 2 },
                new ProductEntity { Name = "C", Price = 3, Quantity = 3 },
            };

            _mockHttpClientHelper.Setup(s => s.GetAsync<IEnumerable<ProductEntity>>(Constants.PRODUCT_API_URL)).ReturnsAsync(products);

            var result = _sut.GetProducts().Result.ToList();

            Assert.AreEqual(products.Count, result.Count);
            for (int i = 0; i < products.Count; i++)
            {
                Assert.AreEqual(products[i].Name, result[i].Name);
                Assert.AreEqual(products[i].Price, result[i].Price);
                Assert.AreEqual(products[i].Quantity, result[i].Quantity);
            }
        }

        [TestMethod]
        public void GetProducts_WhenSortOptionIsLow_ReturnsProductsSortedByPriceLowtoHigh()
        {
            var products = new List<ProductEntity> {
                new ProductEntity { Name = "A", Price = 1, Quantity = 1 },
                new ProductEntity { Name = "B", Price = 2, Quantity = 2 },
                new ProductEntity { Name = "C", Price = 3, Quantity = 3 },
            };
            var expected = products.OrderBy(o => o.Price).ToList();

            _mockHttpClientHelper.Setup(s => s.GetAsync<IEnumerable<ProductEntity>>(Constants.PRODUCT_API_URL)).ReturnsAsync(products);

            var result = _sut.GetProducts(SortOption.Low).Result.ToList();

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, result[i].Name);
                Assert.AreEqual(expected[i].Price, result[i].Price);
                Assert.AreEqual(expected[i].Quantity, result[i].Quantity);
            }
        }

        [TestMethod]
        public void GetProducts_WhenSortOptionIsHigh_ReturnsProductsSortedByPriceHightoLow()
        {
            var products = new List<ProductEntity> {
                new ProductEntity { Name = "A", Price = 1, Quantity = 1 },
                new ProductEntity { Name = "B", Price = 2, Quantity = 2 },
                new ProductEntity { Name = "C", Price = 3, Quantity = 3 },
            };
            var expected = products.OrderByDescending(o => o.Price).ToList();

            _mockHttpClientHelper.Setup(s => s.GetAsync<IEnumerable<ProductEntity>>(Constants.PRODUCT_API_URL)).ReturnsAsync(products);

            var result = _sut.GetProducts(SortOption.High).Result.ToList();

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, result[i].Name);
                Assert.AreEqual(expected[i].Price, result[i].Price);
                Assert.AreEqual(expected[i].Quantity, result[i].Quantity);
            }
        }

        [TestMethod]
        public void GetProducts_WhenSortOptionIsAscending_ReturnsProductsSortedByProductNameAtoZ()
        {
            var products = new List<ProductEntity> {
                new ProductEntity { Name = "A", Price = 1, Quantity = 1 },
                new ProductEntity { Name = "B", Price = 2, Quantity = 2 },
                new ProductEntity { Name = "C", Price = 3, Quantity = 3 },
            };
            var expected = products.OrderBy(o => o.Name).ToList();

            _mockHttpClientHelper.Setup(s => s.GetAsync<IEnumerable<ProductEntity>>(Constants.PRODUCT_API_URL)).ReturnsAsync(products);

            var result = _sut.GetProducts(SortOption.Ascending).Result.ToList();

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, result[i].Name);
                Assert.AreEqual(expected[i].Price, result[i].Price);
                Assert.AreEqual(expected[i].Quantity, result[i].Quantity);
            }
        }

        [TestMethod]
        public void GetProducts_WhenSortOptionIsDescending_ReturnsProductsSortedByProductNameZtoA()
        {
            var products = new List<ProductEntity> {
                new ProductEntity { Name = "A", Price = 1, Quantity = 1 },
                new ProductEntity { Name = "B", Price = 2, Quantity = 2 },
                new ProductEntity { Name = "C", Price = 3, Quantity = 3 },
            };
            var expected = products.OrderByDescending(o => o.Name).ToList();

            _mockHttpClientHelper.Setup(s => s.GetAsync<IEnumerable<ProductEntity>>(Constants.PRODUCT_API_URL)).ReturnsAsync(products);

            var result = _sut.GetProducts(SortOption.Descending).Result.ToList();

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, result[i].Name);
                Assert.AreEqual(expected[i].Price, result[i].Price);
                Assert.AreEqual(expected[i].Quantity, result[i].Quantity);
            }
        }

        [TestMethod]
        public void GetProducts_WhenSortOptionIsRecommended_ReturnsProductsSortedByProductPopularity()
        {
            var products = new List<ProductEntity> {
                new ProductEntity { Name = "A", Price = 1, Quantity = 0 },
                new ProductEntity { Name = "B", Price = 2, Quantity = 0 },
                new ProductEntity { Name = "C", Price = 3, Quantity = 0 }
            };
            var shopperHistory = new List<ShopperHistoryEntity> { 
                new ShopperHistoryEntity { 
                    CustomerId = 1, 
                    Products = new List<ProductEntity> {
                        new ProductEntity { Name = "A", Price = 1, Quantity = 1 },
                        new ProductEntity { Name = "B", Price = 2, Quantity = 1 },
                    } 
                },
                new ShopperHistoryEntity {
                    CustomerId = 2,
                    Products = new List<ProductEntity> {
                        new ProductEntity { Name = "A", Price = 1, Quantity = 1 },
                        new ProductEntity { Name = "C", Price = 2, Quantity = 1 },
                    }
                }
            };

            var expected = shopperHistory
                .SelectMany(s => s.Products)
                .Concat(products)
                .GroupBy(g => g.Name)
                .Select(s => new ProductEntity
                {
                    Name = s.Key,
                    Price = s.Where(w => w.Name == s.Key).First().Price,
                    Quantity = s.Sum(u => u.Quantity)
                })
                .OrderByDescending(o => o.Quantity)
                .ToList(); 

            _mockHttpClientHelper.Setup(s => s.GetAsync<IEnumerable<ProductEntity>>(Constants.PRODUCT_API_URL)).ReturnsAsync(products);
            _mockShopperHistoryProcessor.Setup(s => s.GetShopperHistory()).ReturnsAsync(shopperHistory);

            var result = _sut.GetProducts(SortOption.Recommended).Result.ToList();

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, result[i].Name);
                Assert.AreEqual(expected[i].Price, result[i].Price);
                Assert.AreEqual(expected[i].Quantity, result[i].Quantity);
            }
        }
    }
}
