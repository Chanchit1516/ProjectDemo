using ProjectDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Core.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProduct();
        Task<(int, int, IEnumerable<Product>)> GetAllProductDatatable(string sortColumn,
        string sortColumnDirection,
        int pageSize,
        int skip,
        string searchValue = null,
        IDictionary<string, string> filters = null);

    }
}
