using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WooliesX.Data.Entities;
using WooliesX.Data.Enums;
using WooliesX.Data.Repositories;
using WooliesX.Http;

namespace WooliesX.Data.IntegrationTests
{
    [TestClass]
    public class ProductsProcessorTests
    {
        private IProductsProcessor _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new ProductsProcessor(
                new RestRespository<ProductEntity>(
                    Constants.PRODUCT_API_URL,
                    new HttpClientHelper()
                ),
                new ShopperHistoryProcessor(
                    new RestRespository<ShopperHistoryEntity>(
                        Constants.SHOPPER_HISTORY_API_URL,
                        new HttpClientHelper()
                    )
                )
            );
        }

        [TestMethod]
        public void GetProducts_WithSortOptionAscending_ReturnsProductsWithProductNamesSortedAtoZ()
        {
            var products = _sut.GetProducts(SortOption.Ascending).Result;
            
            Assert.IsNotNull(products);
            Assert.IsTrue(SortTools.IsSorted(products.Select(s => s.Name).ToArray()));
        }

        [TestMethod]
        public void GetProducts_WithSortOptionDescending_ReturnsProductsWithProductNamesSortedZtoA()
        {
            var products = _sut.GetProducts(SortOption.Descending).Result;
            
            Assert.IsNotNull(products);
            Assert.IsTrue(SortTools.IsSortedDescending(products.Select(s => s.Name).ToArray()));
        }

        [TestMethod]
        public void GetProducts_WithSortOptionHigh_ReturnsProductsWithPriceHightoLow()
        {
            var products = _sut.GetProducts(SortOption.High).Result;
            
            Assert.IsNotNull(products);
            Assert.IsTrue(SortTools.IsSortedDescending(products.Select(s => s.Price).ToArray()));
        }

        [TestMethod]
        public void GetProducts_WithSortOptionLow_ReturnsProductsWithPriceLowtoHigh()
        {
            var products = _sut.GetProducts(SortOption.Low).Result;
            
            Assert.IsNotNull(products);
            Assert.IsTrue(SortTools.IsSorted(products.Select(s => s.Price).ToArray()));
        }

        [TestMethod]
        public void GetProducts_WithSortOptionRecommended_ReturnsProductsSortedByPopulrity()
        {
            var products = _sut.GetProducts(SortOption.Recommended).Result;

            Assert.IsNotNull(products);
            Assert.IsTrue(SortTools.IsSortedDescending(products.Select(s => s.Quantity).ToArray()));
        }
    }
}
