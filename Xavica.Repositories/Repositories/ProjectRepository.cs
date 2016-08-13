using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechRadar.DataLayer.Entities;
using TechRadar.DataLayer.Interfaces;

namespace TechRadar.DataLayer.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public IDBContext Context
        {
            get;
            private set;
        }

        public ProjectRepository(IDBContext context)
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
                        var value = this.Context.DB.Execute(" delete from project where Id=@Id", new { Id = key });
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

        public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {
                       // var SearchSql = "select *  from project;";
                        var SearchSql = "select id,name,companyid,programid from project";
                       // SearchSql += "select * from radar;";
                        SearchSql += "select id, name,companyid,programid,projectid from radar;";
                        SearchSql += @"select Radar.Id,Radar.Name,Radar.CompanyId,Radar.ProgramId,Radar.ProjectId,Project.Name as ProjectName
                                     from Radar
                                     join Project on Radar.ProjectId=Project.Id;";
                        SearchSql += @"select radarid,technologyid as TechnnologyId,Name,Quadrant as QuadrantId ,
                                        ring as RingId from RadarTechnologies rt 
                                        join Technology t on rt.TechnologyId = t.Id;";
                        var multi = this.Context.DB.QueryMultiple(SearchSql.ToString());
                        var projects = multi.Read<ProjectEntity>().ToList();
                        var radars = multi.Read<RadarEntity>().ToList();
                        var projectRadars = multi.Read<RadarEntity>().ToList();
                        var radarTechnologies = multi.Read<RadarTechnologyEntity>().ToList();

                        foreach(var project in projects){

                            project.Radars = projectRadars.Where(item => item.ProjectId == project.Id).ToList();

                            foreach (var radar in project.Radars) {

                                radar.Technologies = radarTechnologies.Where(item => item.RadarId ==radar.Id).ToList();

                        }
                        
                        }

                        return projects;

                    });
                    return task;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public Task<PagedResult<ProjectEntity>> GetPagedWhereAsync(PagedParams pagedFilters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectEntity>> GetWhereAsync(IList<SearchParamEntity> filters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(ProjectEntity instance)
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("insert into project(Name,companyId,programId)values(@Name,@companyId,@programId);");
                        sb.Append("select cast(scope_identity() as int)");
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

        public async Task<bool> UpdateAsync(ProjectEntity instance)
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {
                        var reslut = false;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("update project set Name=@Name where Id=@Id");
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


 //var result = this.Context.DB.Query<ProjectEntity>("select * from project").ToList();
 //                       return result;
