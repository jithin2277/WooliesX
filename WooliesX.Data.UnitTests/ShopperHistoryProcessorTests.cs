using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WooliesX.Data.Entities;
using WooliesX.Http;


namespace WooliesX.Data.UnitTests
{
    [TestClass]
    public class ShopperHistoryProcessorTests
    {
        private Mock<IHttpClientHelper> _mockHttpClientHelper;
        private IShopperHistoryProcessor _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpClientHelper = new Mock<IHttpClientHelper>(MockBehavior.Strict);
            _sut = new ShopperHistoryProcessor(_mockHttpClientHelper.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockHttpClientHelper.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenHttpClientHelper_IsNull_ThrowsException()
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

            _mockHttpClientHelper.Setup(s => s.GetAsync<IEnumerable<ShopperHistoryEntity>>(Constants.SHOPPER_HISTORY_API_URL)).ReturnsAsync(expected);
            var result = _sut.GetShopperHistory().Result;

            Assert.AreEqual(expected, result);
        }
    }
}
