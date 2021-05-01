using System.Collections.Generic;

namespace WooliesX.Domain.Entities.Trolley
{
    public class TrolleySpecialEntity
    {
        public IEnumerable<TrolleyProductQuantityEntity> Quantities { get; set; }
        public long Total { get; set; }
    }
}
