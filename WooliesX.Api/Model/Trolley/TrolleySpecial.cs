using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesX.Api.Model.Trolley
{
    public class TrolleySpecial
    {
        public IEnumerable<TrolleyProductQuantity> Quantities { get; set; }
        public long Total { get; set; }
    }
}
