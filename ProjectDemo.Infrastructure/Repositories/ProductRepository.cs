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
                var cmd = @"SELECT * FROMs [dbo].[Product]";
                return await Connection.QueryAsync<Product>(cmd, transaction: Transaction);
            }
            catch (Exception ex)
            {
                throw ex;
                //throw new AppException("GetAllProduct Error:" + ex.Message);
            }
        }
    }
}
