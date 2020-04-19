using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Data.Entities;
using WooliesX.Data.Enums;
using WooliesX.Data.Repositories;

namespace WooliesX.Data
{
    public interface IProductsProcessor : IDisposable
    {
        Task<IEnumerable<ProductEntity>> GetProducts();
        Task<IEnumerable<ProductEntity>> GetProducts(SortOption sortOption);
    }

    public class ProductsProcessor : IProductsProcessor
    {
        private IRepository<ProductEntity> _productRepository;
        private IShopperHistoryProcessor _shopperHistoryProcessor;

        public ProductsProcessor(IRepository<ProductEntity> productRepository, IShopperHistoryProcessor shopperHistoryProcessor)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _shopperHistoryProcessor = shopperHistoryProcessor ?? throw new ArgumentNullException(nameof(shopperHistoryProcessor));
        }

        public async Task<IEnumerable<ProductEntity>> GetProducts()
        {
            return await _productRepository.GetAll().ConfigureAwait(false);
        }

        public async Task<IEnumerable<ProductEntity>> GetProducts(SortOption sortOption)
        {
            if (sortOption == SortOption.Ascending)
            {
                return (await GetProducts().ConfigureAwait(false)).OrderBy(o => o.Name);
            }
            else if (sortOption == SortOption.Descending)
            {
                return (await GetProducts().ConfigureAwait(false)).OrderByDescending(o => o.Name);
            }
            else if (sortOption == SortOption.High)
            {
                return (await GetProducts().ConfigureAwait(false)).OrderByDescending(o => o.Price);
            }
            else if (sortOption == SortOption.Low)
            {
                return (await GetProducts().ConfigureAwait(false)).OrderBy(o => o.Price);
            }
            else if(sortOption == SortOption.Recommended)
            {
                return await _shopperHistoryProcessor.GetProductsByPopularityByQuantity().ConfigureAwait(false);
            }

            return null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_productRepository != null)
                    {
                        _productRepository.Dispose();
                        _productRepository = null;
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
