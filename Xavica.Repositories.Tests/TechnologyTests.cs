using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TechRadar.DataLayer.Interfaces;
using TechRadar.DataLayer.Repositories;
using TechRadar.DataLayer.Entities;
using System.Configuration;
namespace Xavica.Repositories.Tests
{
    [TestClass]
    public class TechnologyTests
    {
        [TestMethod]
        public async Task TestInsertAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            ITechnologyRepository techRepository = new TechnologyRepository(context);
            TechnologyEntity entity = new TechnologyEntity() { Name = "Technology5" };
            var result= await techRepository.InsertAsync(entity);
            await techRepository.DeleteAsync(entity.Id);
            Assert.IsTrue(result == true, "updated");
        }

        [TestMethod]
        public async Task TestGetAllAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            ITechnologyRepository techRepository = new TechnologyRepository(context);
           TechnologyEntity entity = new TechnologyEntity() { Name = "Technology6" };
            await techRepository.InsertAsync(entity);
            await techRepository.GetAllAsync();
          //  Assert.IsTrue(result == true, "GetAll");

        }

        [TestMethod]
        public async Task TestUpdateAsync()
        {

            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            ITechnologyRepository techRepository = new TechnologyRepository(context);
            TechnologyEntity entity = new TechnologyEntity() { Id=1, Name = "Technology7" };
            var result= await techRepository.UpdateAsync(entity);
            Assert.IsTrue(result == true, "updated");

        }

        [TestMethod]
        public async Task TestDeleteAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            ITechnologyRepository techRepository = new TechnologyRepository(context);
            TechnologyEntity entity = new TechnologyEntity() { Name = "Technology8" };
            await techRepository.InsertAsync(entity);
            var result=await techRepository.DeleteAsync(entity.Id);
            Assert.IsTrue(result == true, "deleted");
        }
    }
}
