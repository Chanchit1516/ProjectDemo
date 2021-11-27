using AutoMapper;
using ProjectDemo.Core.DTOs;
using ProjectDemo.Core.DTOs.Home;
using ProjectDemo.Core.Entities;
using ProjectDemo.Core.Interfaces.Repositories;
using ProjectDemo.Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<DataTablesResponse<GetAllProductResponse>> GetAllProductDatatable(DataTablesRequest request)
        {
            try
            {
                var sortColumn = request.Columns[request.Order[0].Column].Name;
                var sortColumnDirection = request.Order[0].Dir;
                int pageSize = request.Length;
                int skip = request.Start;
                var filters = request.Filters.ToDictionary(x => x.Column, x => x.Value);
                var searchValue = request.Search.Value;
                var data = await _unitOfWork.ProductRepository.GetAllProductDatatable(sortColumn, sortColumnDirection, pageSize, skip, null, filters);
                var dataMapper = _mapper.Map<IEnumerable<GetAllProductResponse>>(data.Item3);
                return new DataTablesResponse<GetAllProductResponse>()
                {
                    Draw = request.Draw,
                    RecordsTotal = data.Item1,
                    RecordsFiltered = data.Item2,
                    Data = dataMapper
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
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

    }
}
