using Microsoft.VisualStudio.TestTools.UnitTesting;
using WooliesX.Data.Entities;
using WooliesX.Http;
using System.Linq;

namespace WooliesX.Data.IntegrationTests
{
    [TestClass]
    public class ShopperHistoryProcessorTests
    {
        private IShopperHistoryProcessor _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new ShopperHistoryProcessor(
                new HttpClientHelper()
            );
        }

        [TestMethod]
        public void GetProductsByPopularity_Succeeds()
        {
            var products = _sut.GetProductsByPopularityByQuantity().Result;

            Assert.IsNotNull(products);
            Assert.IsTrue(SortTools.IsSortedDescending(products.Select(s => s.Quantity).ToArray()));
        }
    }
}
