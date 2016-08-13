using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechRadar.DataLayer.Entities;
using TechRadar.DataLayer.Interfaces;
using TechRadar.DataLayer.Repositories;
using System.Configuration;
using System.Threading.Tasks;

namespace Xavica.Repositories.Tests
{
    [TestClass]
    public class CompanyTests
    {
        [TestMethod]
        public async Task InsertTest()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            ICompanyRepository repository = new CompanyRepository(context);
            CompanyEntity entity = new CompanyEntity() { Name="company3"};
            var result=await repository.InsertAsync(entity);
            await  repository.DeleteAsync(entity.Id);
            Assert.IsTrue(result == true, "Insereted");
        }
        [TestMethod]
        public async Task  GetAllTest()
        {
            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            ICompanyRepository repository = new CompanyRepository(context);
            CompanyEntity entity = new CompanyEntity() { Name = "company4" };
            await repository.InsertAsync(entity);
            var lstcompanies=  await repository.GetAllAsync();
           // Assert.IsTrue(lstcompanies. > 0, "GetAll");
        }

        [TestMethod]
        public async Task TestUpdate() {


            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            ICompanyRepository repository = new CompanyRepository(context);
            CompanyEntity entity = new CompanyEntity() {Id=2, Name = "company5" };
            var result= await repository.UpdateAsync(entity);
           Assert.IsTrue(result == true, "Updated");


        }
        [TestMethod]
        public async Task TestDelete()
        {


            IDBContext context = new DBContext(ConfigurationManager.ConnectionStrings["mcs"].ConnectionString);
            ICompanyRepository repository = new CompanyRepository(context);
            CompanyEntity entity = new CompanyEntity() { Name = "company6" };
            await repository.InsertAsync(entity);
           var result= await repository.DeleteAsync(entity.Id);
            Assert.IsTrue(result == true, "Deleted");

        }

    }
}
