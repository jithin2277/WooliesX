using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Data.Entities;
using WooliesX.Data.Enums;
using WooliesX.Data.Repositories;
using System.Linq;

namespace WooliesX.Data
{
    public interface IShopperHistoryProcessor : IDisposable
    {
        Task<IEnumerable<ShopperHistoryEntity>> GetShopperHistory();
        Task<IEnumerable<ProductEntity>> GetProductsByPopularityByQuantity();
        Task<IEnumerable<ProductEntity>> GetProductsByPopularityByFrequency();
    }

    public class ShopperHistoryProcessor : IShopperHistoryProcessor
    {
        private IRepository<ShopperHistoryEntity> _shopperHistoryRepository;

        public ShopperHistoryProcessor(IRepository<ShopperHistoryEntity> shopperHistoryRepository)
        {
            _shopperHistoryRepository = shopperHistoryRepository
                ?? throw new ArgumentNullException(nameof(shopperHistoryRepository));
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsByPopularityByQuantity()
        {
            var shopperHistory = await GetShopperHistory().ConfigureAwait(false);

            return shopperHistory
                .SelectMany(s => s.Products)
                .GroupBy(g => g.Name)
                .Select(s => new ProductEntity {
                    Name = s.Key,
                    Price = s.Where(w => w.Name == s.Key).First().Price,
                    Quantity = s.Sum(u => u.Quantity)
                })
                .OrderByDescending(o => o.Quantity);
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsByPopularityByFrequency()
        {
            var shopperHistory = await GetShopperHistory().ConfigureAwait(false);

            return shopperHistory
                .SelectMany(s => s.Products)
                .GroupBy(g => g.Name)
                .Select(s => new { Product = s.FirstOrDefault(), Count = s.Count() })
                .OrderByDescending(o => o.Count)
                .Select(s => s.Product);
        }

        public async Task<IEnumerable<ShopperHistoryEntity>> GetShopperHistory()
        {
            return await _shopperHistoryRepository.GetAll().ConfigureAwait(false);
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_shopperHistoryRepository != null)
                    {
                        _shopperHistoryRepository.Dispose();
                        _shopperHistoryRepository = null;
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
