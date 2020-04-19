using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WooliesX.Data.Entities;
using WooliesX.Data.Repositories;
using WooliesX.Http;


namespace WooliesX.Data.UnitTests
{
    [TestClass]
    public class ShopperHistoryProcessorTests
    {
        private Mock<IRepository<ShopperHistoryEntity>> _mockShopperHistoryRepository;
        private IShopperHistoryProcessor _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockShopperHistoryRepository = new Mock<IRepository<ShopperHistoryEntity>>(MockBehavior.Strict);
            _sut = new ShopperHistoryProcessor(_mockShopperHistoryRepository.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockShopperHistoryRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenShopperHistoryRepository_IsNull_ThrowsException()
        {
            _ = new ShopperHistoryProcessor(null);
        }

        [TestMethod]
        public void GetShopperHistory_ReturnsAllShopperHistory()
        {
            var expected = new List<ShopperHistoryEntity> {
                new ShopperHistoryEntity {
                    CustomerId = 123,
                    Products = new List<ProductEntity> {
                        new ProductEntity {  Name = "A", Price = 1, Quantity = 1},
                        new ProductEntity {  Name = "B", Price = 2, Quantity = 1},
                        new ProductEntity {  Name = "C", Price = 3, Quantity = 1}
            }}};

            _mockShopperHistoryRepository.Setup(s => s.GetAll()).ReturnsAsync(expected);
            var result = _sut.GetShopperHistory().Result;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetProductsByPopularity_ReturnsProductsByPopularity()
        {
            var data = new List<ShopperHistoryEntity> {
                new ShopperHistoryEntity {
                    CustomerId = 123,
                    Products = new List<ProductEntity> {
                        new ProductEntity {  Name = "A", Price = 1, Quantity = 1},
                        new ProductEntity {  Name = "B", Price = 2, Quantity = 1},
                        new ProductEntity {  Name = "C", Price = 3, Quantity = 3}
                    }
                },
                new ShopperHistoryEntity {
                    CustomerId = 23,
                    Products = new List<ProductEntity> {
                        new ProductEntity {  Name = "A", Price = 1, Quantity = 1},
                    }
                },
                new ShopperHistoryEntity {
                    CustomerId = 23,
                    Products = new List<ProductEntity> {
                        new ProductEntity {  Name = "B", Price = 1, Quantity = 1},
                    }
                },
                new ShopperHistoryEntity {
                    CustomerId = 23,
                    Products = new List<ProductEntity> {
                        new ProductEntity {  Name = "B", Price = 2, Quantity = 2},
                        new ProductEntity {  Name = "C", Price = 3, Quantity = 3}
                    }
                }
            };

            var expected = data.SelectMany(s => s.Products)
                .GroupBy(g => g.Name)
                .Select(s => {
                    return new ProductEntity
                    {
                        Name = s.Key,
                        Price = s.Sum(p => p.Price),
                        Quantity = s.Count()
                    };
                })
                .OrderByDescending(o => o.Quantity).ToList();

            _mockShopperHistoryRepository.Setup(s => s.GetAll()).ReturnsAsync(data);
            var result = _sut.GetProductsByPopularity().Result.ToList();

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, result[i].Name);
                Assert.AreEqual(expected[i].Price, result[i].Price);
                Assert.AreEqual(expected[i].Quantity, result[i].Quantity);
            }
        }
    }
}
