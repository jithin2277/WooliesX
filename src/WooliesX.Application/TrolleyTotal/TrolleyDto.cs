using System.Collections.Generic;
using WooliesX.Application.Common.Mappings;
using WooliesX.Domain.Entities.Trolley;

namespace WooliesX.Application.TrolleyTotal
{
    public class TrolleyDto : IMapTo<TrolleyEntity>
    {
        public IEnumerable<TrolleyProduct> Products { get; set; }
        public IEnumerable<TrolleySpecial> Specials { get; set; }
        public IEnumerable<TrolleyProductQuantity> Quantities { get; set; }
    }

    public class TrolleyProduct : IMapTo<TrolleyProductEntity>
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class TrolleySpecial : IMapTo<TrolleySpecialEntity>
    {
        public IEnumerable<TrolleyProductQuantity> Quantities { get; set; }
        public double Total { get; set; }
    }

    public class TrolleyProductQuantity : IMapTo<TrolleyProductQuantityEntity>
    {
        public string Name { get; set; }
        public long Quantity { get; set; }
    }
}
