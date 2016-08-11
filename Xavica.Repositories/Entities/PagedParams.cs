using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechRadar.DataLayer.Entities
{
    public class PagedParams
    {
        public Int32 PageNumber { get; set; }
        public Int32 PageSize { get; set; }
        public IList<SearchParamEntity> Filters { get; set; }
    }
}
