using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechRadar.DataLayer.Entities;

namespace TechRadar.DataLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IDBContext Context { get; }
        
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<IEnumerable<T>> GetWhereAsync(IList<SearchParamEntity> filters);

        Task<PagedResult<T>> GetPagedWhereAsync(PagedParams pagedFilters);

        Task<bool> InsertAsync(T instance);

        Task<bool> UpdateAsync(T instance);

        Task<bool> DeleteAsync(object key);

    }
}
