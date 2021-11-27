using Dapper;
using ProjectDemo.Application.Helpers;
using ProjectDemo.Core.Entities;
using ProjectDemo.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(SqlTransaction transaction) : base(transaction)
        { }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            try
            {
                var cmd = @"SELECT * FROM [dbo].[Product]";
                return await Connection.QueryAsync<Product>(cmd, transaction: Transaction);
            }
            catch (Exception ex)
            {
                throw ex;
                //throw new AppException("GetAllProduct Error:" + ex.Message);
            }
        }

        public async Task<(int, int, IEnumerable<Product>)> GetAllProductDatatable(string sortColumn, string sortColumnDirection, int pageSize, int skip, string searchValue = null, IDictionary<string, string> filters = null)
        {
            string orderBy = $"ORDER BY {sortColumn} {sortColumnDirection}";
            string paging = $"OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY";
            string where = "";
            dynamic parameters = new System.Dynamic.ExpandoObject();
            filters ??= new Dictionary<string, string>();
            foreach (var item in filters)
            {
                if (!string.IsNullOrEmpty(item.Value) && item.Key == "Name")
                {
                    where = $" WHERE name LIKE @Name";
                    parameters.Name = $"%{item.Value}%";
                }
            }
            try
            {
                string sqlBase = $@"SELECT COUNT(*) FROM [dbo].[Product]";
                string sqlCount = $@"{sqlBase}";
                var cmd = @"SELECT * FROM [dbo].[Product]";
                var countTotal = await Connection.QueryFirstOrDefaultAsync<int>(sqlCount, transaction: Transaction);
                var countFiltered = await Connection.QueryFirstOrDefaultAsync<int>($"{sqlBase} {where}", (object)parameters, transaction: Transaction);
                var result = await Connection.QueryAsync<Product>($"{cmd} {where} {orderBy} {paging}", (object)parameters, transaction: Transaction);
                return (countTotal, countFiltered, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
