using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TechRadar.DataLayer.Interfaces;
using TechRadar.DataLayer.Repositories;
using TechRadar.DataLayer.Entities;
using System.Configuration;
using System.Collections.Generic;

namespace Xavica.Repositories.Tests
{
    [TestClass]
    public class RadarTests
    {
        [TestMethod]
        public async Task TestInsertAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IRadarRepository radarRepository = new RadarRepository(context);
            RadarEntity entity = new RadarEntity() { Name = "Radar7",CompanyId=1,ProjectId=1 };
            var result=await radarRepository.InsertAsync(entity);
            await radarRepository.DeleteAsync(entity.Id);
            Assert.IsTrue(result == true, "inserted");
            
        }

        [TestMethod]
        public async Task TestGetAllAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IRadarRepository radarRepository = new RadarRepository(context);
           RadarEntity entity = new RadarEntity() { Name = "Radar8", ProgramId = 2, ProjectId = 1};
           await radarRepository.InsertAsync(entity);
            var lstradars=await radarRepository.GetAllAsync();
         
            //Assert.IsTrue(result == true, "Insereted");
        }

        [TestMethod]
        public async Task TestUpdateAsync()
        {

            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IRadarRepository radarRepository = new RadarRepository(context);
            RadarEntity entity = new RadarEntity() { Id=2,Name = "Radar9", CompanyId = 2, ProjectId = 1 };
            var result=await radarRepository.UpdateAsync(entity);
            Assert.IsTrue(result == true, "updated");
        }

        [TestMethod]
        public async Task TestDeleteAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IRadarRepository radarRepository = new RadarRepository(context);
            RadarEntity entity = new RadarEntity() {Name = "radar10", ProgramId = 3, ProjectId = 1 };
            await radarRepository.InsertAsync(entity);
            var result= await radarRepository.DeleteAsync(entity.Id);
            Assert.IsTrue(result == true, "deleted");

        }

    }
}
