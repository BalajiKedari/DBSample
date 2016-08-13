using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using TechRadar.DataLayer.Entities;
using TechRadar.DataLayer.Interfaces;
using TechRadar.DataLayer.Repositories;
using System.Threading.Tasks;

namespace Xavica.Repositories.Tests
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public async Task TestInsertAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IProgramRepository repository = new ProgramRepository(context);
            ProgramEntity entity = new ProgramEntity() { Name = "Program4", companyId=1 };
            var result = await repository.InsertAsync(entity);
            await  repository.DeleteAsync(entity.Id);
            Assert.IsTrue(result == true, "Insereted");
        }

        [TestMethod]
        public async Task TestGetAllAsync()
        {

            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IProgramRepository repository = new ProgramRepository(context);
            ProgramEntity entity = new ProgramEntity() { Name = " Program5", companyId = 2 };
            await repository.InsertAsync(entity);
            await repository.GetAllAsync();
            // Assert.IsTrue(, "GetAll");

        }

        [TestMethod]
        public async Task TestUpdateAsync()
        {

            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IProgramRepository repository = new ProgramRepository(context);
            ProgramEntity entity = new ProgramEntity() { Id = 1, Name = "Program6", companyId = 3 };
            var result= await repository.UpdateAsync(entity);
            Assert.IsTrue(result == true, "updated");
        }

        [TestMethod]
        public async Task TestDeleteAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IProgramRepository repository = new ProgramRepository(context);
            ProgramEntity entity = new ProgramEntity() { Name = "program7", companyId = 2 };
            await repository.InsertAsync(entity);
            var result=await repository.DeleteAsync(entity.Id);
            Assert.IsTrue(result == true, "deleted");
        }
    }
}
