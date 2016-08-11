using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechRadar.DataLayer.Entities
{
    public class PagedResult<T> where T : class
    {
        public int QueryCount { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
