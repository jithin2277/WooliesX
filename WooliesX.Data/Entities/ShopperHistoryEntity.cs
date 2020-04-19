using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesX.Data.Entities
{
    public class ShopperHistoryEntity
    {
        public int CustomerId { get; set; }
        public IEnumerable<ProductEntity> Products { get; set; }
    }
}
