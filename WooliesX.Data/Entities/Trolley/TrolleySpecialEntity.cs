using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesX.Data.Entities.Trolley
{
    public class TrolleySpecialEntity
    {
        public IEnumerable<TrolleyProductQuantityEntity> Quantities { get; set; }
        public long Total { get; set; }
    }
}
