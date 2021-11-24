using ProjectDemo.Core.DTOs.Home;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Core.Interfaces.UseCases
{
    public interface IHomeService
    {
        Task<IEnumerable<GetAllProductResponse>> GetAllProduct();
        Task<int> AddProductTest(AddProductTestRequest request);
    }
}
