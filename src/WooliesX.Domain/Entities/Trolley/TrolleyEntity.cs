using System.Collections.Generic;

namespace WooliesX.Domain.Entities.Trolley
{
    public class TrolleyEntity
    {
        public IEnumerable<TrolleyProductEntity> Products { get; set; }
        public IEnumerable<TrolleySpecialEntity> Specials { get; set; }
        public IEnumerable<TrolleyProductQuantityEntity> Quantities { get; set; }
    }
}
