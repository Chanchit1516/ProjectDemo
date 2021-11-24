using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Core.Interfaces.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByKeyAsync(string column, string value);
        Task<int> DeleteRowAsync(int id);
        Task<int> InsertAsync(T t);
        Task<int> InsertRangeAsync(IEnumerable<T> list);
        Task<int> UpdateAsync(T t);
        Task<int> UpdateRangeAsync(IEnumerable<T> list);

    }
}
