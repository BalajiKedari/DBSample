using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechRadar.DataLayer.Entities;
using TechRadar.DataLayer.Interfaces;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TechRadar.DataLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        public IDBContext Context
        {
            get;
            private set;

        }

        public CompanyRepository(IDBContext context)
        {
            Context = context;

        }
        public async Task<bool> DeleteAsync(object key)
        {
            // throw new NotImplementedException();
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {

                        var value = this.Context.DB.Execute(" delete from company where Id=@Id", new { Id = key });
                        return value > 0 ? true : false;

                    });
                    return task;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<CompanyEntity>> GetAllAsync()
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {

                        var SearchSql = "select id,name from company;";
                        SearchSql += "select id, name,companyid,programid,projectid from radar;";
                        SearchSql += "select id,name,companyid from program;";
                        SearchSql += "select id,name,companyid,programid from project;";
                        SearchSql += @"select radar.Id,radar.Name,radar.CompanyId,
                                     radar.ProgramId,radar.projectId
                                     from radar
                                     join Company on Radar.CompanyId=Company.Id;";
                        SearchSql += @"select radarid,technologyid as TechnnologyId,Name,Quadrant as QuadrantId ,
                                        ring as RingId from RadarTechnologies rt 
                                        join Technology t on rt.TechnologyId = t.Id;";
                        SearchSql += @"select Radar.Id,Radar.Name,Radar.CompanyId,Radar.ProgramId,Radar.ProjectId,Project.Name as ProjectName
                                     from Radar
                                     join Project on Radar.ProjectId=Project.Id;";

                        var multi = this.Context.DB.QueryMultiple(SearchSql.ToString());
                        var companies = multi.Read<CompanyEntity>().ToList();
                        var radars = multi.Read<RadarEntity>().ToList();
                        var programs = multi.Read<ProgramEntity>().ToList();
                        var projects = multi.Read<ProjectEntity>().ToList();
                        var companyRadars = multi.Read<RadarEntity>().ToList();
                        var radarTechnologies = multi.Read<RadarTechnologyEntity>().ToList();
                        var projectRadars = multi.Read<RadarEntity>().ToList();

                        foreach (var company in companies)
                        {
                            company.Projects = projects.Where(item => item.CompanyId == company.Id).ToList();

                            foreach (var project in company.Projects)
                            {
                                project.Radars = projectRadars.Where(item => item.ProjectId == project.Id).ToList();

                                foreach (var radar in project.Radars)
                                {
                                    radar.Technologies = radarTechnologies.Where(item => item.RadarId == radar.Id).ToList();

                                }

                            }

                            company.Programs = programs.Where(item => item.companyId == company.Id).ToList();

                            foreach (var program in company.Programs)
                            {

                                program.Radars = radars.Where(item => item.ProgramId == program.Id).ToList();

                                foreach (var radar in program.Radars)
                                {

                                    radar.Technologies = radarTechnologies.Where(item => item.RadarId == radar.Id).ToList();

                                }

                            }

                            company.Radars = companyRadars.Where(item => item.CompanyId == company.Id).ToList();

                            foreach (var CompanyRadar in company.Radars)
                            {

                                CompanyRadar.Technologies = radarTechnologies.Where(item => item.RadarId == CompanyRadar.Id).ToList();

                            }

                        }

                        return companies;
                    });
                    return task;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public Task<PagedResult<CompanyEntity>> GetPagedWhereAsync(PagedParams pagedFilters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CompanyEntity>> GetWhereAsync(IList<SearchParamEntity> filters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(CompanyEntity instance)
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("insert into Company values(@Name);");
                        sb.Append("select cast(scope_identity() as int)");
                        var returnValue = this.Context.DB.Query<int>(sb.ToString(), instance).Single();
                        instance.Id = returnValue;
                        if (returnValue > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    });
                    return task;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public async Task<bool> UpdateAsync(CompanyEntity instance)
        {
            // throw new NotImplementedException();
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {
                        var reslut = false;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("update Company set Name=@Name where Id=@Id");
                        var record = this.Context.DB.Execute(sb.ToString(), instance);
                        if (record > 0)
                        {
                            reslut = true;
                        }
                        return reslut;
                    });
                    return task;
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
    }
}
//var result = this.Context.DB.Query<CompanyEntity>("select * from company").ToList();
//return result;