using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechRadar.DataLayer.Entities
{
    public class ProjectEntity
    {
        public IList<RadarEntity> Radars { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
       //new
        public int CompanyId { get; set; }
        public int programId { get; set; }

    }
}
