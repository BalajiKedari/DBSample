using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechRadar.DataLayer.Entities
{
    public class CompanyEntity
    {
        public IList<RadarEntity> Radars { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ProgramEntity> Programs { get; set; }
        public IList<ProjectEntity> Projects { get; set; }

    }
}
