using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesX.Api.Model.Trolley
{
    public class Trolley
    {
        public IEnumerable<TrolleyProduct> Products { get; set; }
        public IEnumerable<TrolleySpecial> Specials { get; set; }
        public IEnumerable<TrolleyProductQuantity> Quantities { get; set; }
    }
}
