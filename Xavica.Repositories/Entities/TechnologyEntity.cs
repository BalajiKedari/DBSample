using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechRadar.DataLayer.Entities
{
    public class TechnologyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuadrantId { get; set; }
        public int RingId { get; set; }
        

    }
}
