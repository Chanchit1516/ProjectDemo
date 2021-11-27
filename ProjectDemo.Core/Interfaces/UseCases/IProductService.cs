using ProjectDemo.Core.DTOs;
using ProjectDemo.Core.DTOs.Home;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Core.Interfaces.UseCases
{
    public interface IProductService
    {
        Task<IEnumerable<GetAllProductResponse>> GetAllProduct();
        Task<DataTablesResponse<GetAllProductResponse>> GetAllProductDatatable(DataTablesRequest request);
        Task<int> AddProductTest(AddProductTestRequest request);
    }
}
