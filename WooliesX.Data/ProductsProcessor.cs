using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Data.Entities;
using WooliesX.Data.Enums;
using WooliesX.Http;

namespace WooliesX.Data
{
    public interface IProductsProcessor : IDisposable
    {
        Task<IEnumerable<ProductEntity>> GetProducts();
        Task<IEnumerable<ProductEntity>> GetProducts(SortOption sortOption);
    }

    public class ProductsProcessor : IProductsProcessor
    {
        private IHttpClientHelper _httpClientHelper;
        private IShopperHistoryProcessor _shopperHistoryProcessor;

        public ProductsProcessor(IHttpClientHelper httpClientHelper, IShopperHistoryProcessor shopperHistoryProcessor)
        {
            _httpClientHelper = httpClientHelper ?? throw new ArgumentNullException(nameof(httpClientHelper));
            _shopperHistoryProcessor = shopperHistoryProcessor ?? throw new ArgumentNullException(nameof(shopperHistoryProcessor));
        }

        public async Task<IEnumerable<ProductEntity>> GetProducts()
        {
            return await _httpClientHelper
                .GetAsync<IEnumerable<ProductEntity>>(Constants.PRODUCT_API_URL)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<ProductEntity>> GetProducts(SortOption sortOption)
        {
            var allProducts = await GetProducts().ConfigureAwait(false);

            if (sortOption == SortOption.Ascending)
            {
                return allProducts.OrderBy(o => o.Name);
            }
            else if (sortOption == SortOption.Descending)
            {
                return allProducts.OrderByDescending(o => o.Name);
            }
            else if (sortOption == SortOption.High)
            {
                return allProducts.OrderByDescending(o => o.Price);
            }
            else if (sortOption == SortOption.Low)
            {
                return allProducts.OrderBy(o => o.Price);
            }
            else if(sortOption == SortOption.Recommended)
            {
                return await SortProductsByPopularity(allProducts).ConfigureAwait(false);
            }

            return null;
        }

        private async Task<IEnumerable<ProductEntity>> SortProductsByPopularity(IEnumerable<ProductEntity> allProducts)
        {
            var shopperHistory = await _shopperHistoryProcessor.GetShopperHistory().ConfigureAwait(false);

            return shopperHistory
                .SelectMany(s => s.Products)
                .Concat(allProducts)
                .GroupBy(g => g.Name)
                .Select(s => new ProductEntity
                {
                    Name = s.Key,
                    Price = s.Where(w => w.Name == s.Key).First().Price,
                    Quantity = s.Sum(u => u.Quantity)
                })
                .OrderByDescending(o => o.Quantity);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_httpClientHelper != null)
                    {
                        _httpClientHelper = null;
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
