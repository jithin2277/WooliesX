using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Entities;
using WooliesX.Domain.Enums;

namespace WooliesX.Application.Core
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductEntity>> GetProducts(SortOption? sortOption);
        Task<IEnumerable<ProductEntity>> SortByRecommended(IEnumerable<ProductEntity> products);
    }

    public class ProductsService : IProductsService
    {
        private readonly IProductRespository _productRespository;
        private readonly IShopperRespository _shopperRespository;

        public ProductsService(IProductRespository productRespository, IShopperRespository shopperRespository)
        {
            _productRespository = productRespository;
            _shopperRespository = shopperRespository;
        }

        public async Task<IEnumerable<ProductEntity>> SortByRecommended(IEnumerable<ProductEntity> products)
        {
            var shopperHistory = await _shopperRespository.GetShopperHistory().ConfigureAwait(false);

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

        public async Task<IEnumerable<ProductEntity>> GetProducts(SortOption? sortOption)
        {
            var products = await _productRespository.GetProducts().ConfigureAwait(false);
            if (sortOption.HasValue)
            {
                return sortOption.Value switch
                {
                    SortOption.Low => products.OrderBy(o => o.Price),
                    SortOption.High => products.OrderByDescending(o => o.Price),
                    SortOption.Ascending => products.OrderBy(o => o.Name),
                    SortOption.Descending => products.OrderByDescending(o => o.Name),
                    SortOption.Recommended => await SortByRecommended(products).ConfigureAwait(false),
                    _ => products,
                };
            }

            return products;
        }
    }
}
