using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesX.Data.Entities.Trolley
{
    public class TrolleyEntity
    {
        public IEnumerable<TrolleyProductEntity> Products { get; set; }
        public IEnumerable<TrolleySpecialEntity> Specials { get; set; }
        public IEnumerable<TrolleyProductQuantityEntity> Quantities { get; set; }
    }
}
