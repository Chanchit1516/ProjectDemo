using AutoMapper;
using ProjectDemo.Core.DTOs.Home;
using ProjectDemo.Core.Entities;
using ProjectDemo.Core.Interfaces.Repositories;
using ProjectDemo.Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Application.Services
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddProductTest(AddProductTestRequest request)
        {
            try
            {
                var product = _mapper.Map<Product>(request);
                var response = await _unitOfWork.ProductRepository.InsertAsync(product);

                _unitOfWork.Commit();
                return response;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<IEnumerable<GetAllProductResponse>> GetAllProduct()
        {
            try
            {
                var response = await _unitOfWork.ProductRepository.GetAllProduct();

                var dataMapper = _mapper.Map<IEnumerable<GetAllProductResponse>>(response);
                return dataMapper;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }
    }
}
