using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechRadar.DataLayer.Entities
{
    public class RadarEntity
    {
        public IList<RadarTechnologyEntity> Technologies { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public int? ProgramId { get; set; }
        public int? ProjectId { get; set; }

    }
}
