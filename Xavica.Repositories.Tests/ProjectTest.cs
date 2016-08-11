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
    public class ProjectTest
    {
        [TestMethod]
        public async Task TestInsertAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IProjectRepository repository = new ProjectRepository(context);
            ProjectEntity entity = new ProjectEntity() { Name = "Project1" };
            var result=await repository.InsertAsync(entity);
            await repository.DeleteAsync(entity.Id);
            Assert.IsTrue(result == true, "inserted");
        }

        [TestMethod]
        public async Task TestGetAllAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IProjectRepository repository = new ProjectRepository(context);
            ProjectEntity entity = new ProjectEntity() { Name = "Project2" };
            await repository.InsertAsync(entity);
            await repository.GetAllAsync();
           // Assert.IsTrue(result == true, "GetAll");

        }

        [TestMethod]
        public async Task TestUpdateAsync()
        {

            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IProjectRepository repository = new ProjectRepository(context);
            ProjectEntity entity = new ProjectEntity() {Id=2, Name = "Project3" };
            var result=await repository.UpdateAsync(entity);
            Assert.IsTrue(result == true, "updated");

        }

        [TestMethod]
        public async Task TestDeleteAsync()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            IProjectRepository repository = new ProjectRepository(context);
            ProjectEntity entity = new ProjectEntity() { Name = "Project4" };
            await repository.InsertAsync(entity);
            var result= await repository.DeleteAsync(entity.Id);
            Assert.IsTrue(result == true, "deleted");

        }

    }
}
