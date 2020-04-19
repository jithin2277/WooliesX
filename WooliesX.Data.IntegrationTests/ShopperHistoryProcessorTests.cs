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
        public void GetShopperHistory_Succeeds()
        {
            var shopperHistory = _sut.GetShopperHistory().Result;

            Assert.IsNotNull(shopperHistory);
        }
    }
}
