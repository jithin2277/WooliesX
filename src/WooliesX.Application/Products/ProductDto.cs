using WooliesX.Application.Common.Mappings;
using WooliesX.Domain.Entities;

namespace WooliesX.Application.Products
{
    public class ProductDto : IMapFrom<ProductEntity>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public long Quantity { get; set; }
    }
}
