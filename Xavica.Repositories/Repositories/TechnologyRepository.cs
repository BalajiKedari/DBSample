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
    public class TechnologyRepository : ITechnologyRepository
    {
        public IDBContext Context
        {
            get;
            private set;
            
        }

        public TechnologyRepository(IDBContext context) {
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

                        var value = this.Context.DB.Execute("delete from technology where @Id=Id", new { Id = key });

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

        public async Task<IEnumerable<TechnologyEntity>> GetAllAsync()
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {
                        var result = this.Context.DB.Query<TechnologyEntity>("select id,name from technology").ToList();
                        return result; ;

                       
                    });

                    return task;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public Task<PagedResult<TechnologyEntity>> GetPagedWhereAsync(PagedParams pagedFilters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TechnologyEntity>> GetWhereAsync(IList<SearchParamEntity> filters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(TechnologyEntity instance)
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("insert into technology(Name) values(@Name);");
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

        public async Task<bool> UpdateAsync(TechnologyEntity instance)
        {
            using (Context)
            {
                try
                {
                    var task = await Task.Run(() =>
                    {

                        var result = false;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("update technology set Name=@Name where Id=@Id");
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


