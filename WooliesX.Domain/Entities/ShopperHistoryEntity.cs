using System.Collections.Generic;

namespace WooliesX.Domain.Entities
{
    public class ShopperHistoryEntity
    {
        public int CustomerId { get; set; }
        public IEnumerable<ProductEntity> Products { get; set; }
    }
}
