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
    public class RadarRepository : IRadarRepository
    {
        public IDBContext Context
        {
            get;
            private set;
        }
        public RadarRepository(IDBContext context)
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

                        var value = this.Context.DB.Execute("delete from radar where @Id=Id", new { Id = key });

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

        public async Task<IEnumerable<RadarEntity>> GetAllAsync()
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {

                        var searchSQL = "select id, name,companyid,programid,projectid from radar;";
                        searchSQL +=@"select radarid,technologyid as TechnnologyId,Name,Quadrant as QuadrantId ,
                                        ring as RingId from RadarTechnologies rt 
                                        join Technology t on rt.TechnologyId = t.Id";
                        
                        var multi = this.Context.DB.QueryMultiple(searchSQL.ToString());
                        var radars = multi.Read<RadarEntity>().ToList();
                        var radarTechnologies = multi.Read<RadarTechnologyEntity>().ToList();
                        foreach(var radar in radars)
                        {
                            radar.Technologies = radarTechnologies.Where(item => item.RadarId == radar.Id).ToList();
                        }

                        return radars ; 
                    });

                    return task; 
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public Task<PagedResult<RadarEntity>> GetPagedWhereAsync(PagedParams pagedFilters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RadarEntity>> GetWhereAsync(IList<SearchParamEntity> filters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(RadarEntity instance)
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("insert into radar(Name,companyId,programId,projectId) values(@Name,@companyId,@programId,@projectId);");
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

        public async Task<bool> UpdateAsync(RadarEntity instance)
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {

                        var result = false;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("update radar set Name=@Name,companyId=@companyId,programId=@programId,projectId=@projectId where Id=@Id");
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

