using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechRadar.DataLayer.Entities;
using TechRadar.DataLayer.Interfaces;

namespace TechRadar.DataLayer.Repositories
{
    public class ProgramRepository : IProgramRepository
    {

        public IDBContext Context
        {
            get;
            private set;

        }

        public ProgramRepository(IDBContext context)
        {
            Context = context;

        }


        public async Task<bool> DeleteAsync(object key)
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {
                        var value = this.Context.DB.Execute(" delete from program where Id=@Id", new { Id = key });
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

        public async Task<IEnumerable<ProgramEntity>> GetAllAsync()
        {

            using (Context)
            {

                try
                {
                   
                    var task = await Task.Run(() =>
                    {
                        var SearchSql = "select id,name,companyid from program;";
                        SearchSql += "select select id, name,companyid,programid,projectid from radar;";
                        SearchSql += "select select id,name,companyid,programid from project";
                        SearchSql += @"select Radar.Id,Radar.Name,Radar.CompanyId,Radar.ProgramId,Radar.ProjectId
                                     from Radar 
                                     join Program on Radar.ProgramId=Program.Id ";

                        SearchSql += @"select project.Id,Project.Name,Project.CompanyId,project.Programid
                                       from Project
                                       join program on Program.Id = project.ProgramId;";
                        SearchSql += @"select radarid,technologyid as TechnnologyId,Name,Quadrant as QuadrantId ,
                                        ring as RingId from RadarTechnologies rt 
                                        join Technology t on rt.TechnologyId = t.Id;";

                        var multi = this.Context.DB.QueryMultiple(SearchSql.ToString());
                        var programs = multi.Read<ProgramEntity>().ToList();
                        var radars=multi.Read<RadarEntity>().ToList(); 
                        var projects = multi.Read<ProjectEntity>().ToList();
                        var programRadars = multi.Read<RadarEntity>().ToList();
                        var programProjects = multi.Read<ProjectEntity>().ToList();
                        var radarTechnologies = multi.Read<RadarTechnologyEntity>().ToList();

                        foreach (var program in programs){

                             program.Projects= programProjects.Where(item=>item.programId==program.Id).ToList();

                            foreach (var project in program.Projects) {

                                project.Radars = radars.Where(item => item.ProjectId == project.Id).ToList();

                                foreach (var radar in project.Radars) {

                                    radar.Technologies = radarTechnologies.Where(item => item.RadarId == radar.Id).ToList();

                                }
                            }

                            program.Radars = radars.Where(item => item.ProgramId == program.Id).ToList();

                            foreach (var radar in program.Radars) {

                                radar.Technologies = radarTechnologies.Where(item => item.RadarId == radar.Id).ToList();

                            }

                        }

                         return programs;

                    });
                    return task;

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }

        public Task<PagedResult<ProgramEntity>> GetPagedWhereAsync(PagedParams pagedFilters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProgramEntity>> GetWhereAsync(IList<SearchParamEntity> filters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(ProgramEntity instance)
        {
            using (Context)
            {
                try
                {

                    var task = await Task.Run(() =>
                     {

                         StringBuilder sb = new StringBuilder();
                         sb.Append("insert into program(Name,companyId) values(@Name,@companyId);");
                         sb.Append("select cast(scope_identity() as int);");
                         var returnValue = this.Context.DB.Query<int>(sb.ToString(), instance).Single();
                         instance.Id = returnValue;
                         if (returnValue > 0)
                         {
                             return true;

                         }
                         else {
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




        public async Task<bool> UpdateAsync(ProgramEntity instance)
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                     {

                         var result = false;
                         StringBuilder sb = new StringBuilder();
                         sb.Append("update program set Name=@Name,companyid=@companyId where Id=@Id");
                         var record = this.Context.DB.Execute(sb.ToString(), instance);
                         if (record > 0)
                         {
                             result = true;
                         }

                         return result;

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


 //var result = this.Context.DB.Query<ProgramEntity>(" select * from program").ToList();
 //                       return result;