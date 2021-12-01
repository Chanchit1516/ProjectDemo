using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectDemo.Api.Filters;
using ProjectDemo.Application.Helpers;
using ProjectDemo.Core.DTOs;
using ProjectDemo.Core.DTOs.Home;
using ProjectDemo.Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _homeService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService homeService, ILogger<ProductController> logger)
        {
            _homeService = homeService;
            _logger = logger;

        }

        [HttpPost]
        [ValidateModel]
        [Route("AddProductTest")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddProductTest([FromBody] AddProductTestRequest request)
        {
            var response = await _homeService.AddProductTest(request);
            return Ok(response);
        }

        [HttpGet]
        [ValidateModel]
        [Route("GetAllProduct")]
        [ProducesResponseType(typeof(IEnumerable<GetAllProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProduct()
        {
            var response = await _homeService.GetAllProduct();
            return Ok(response);
        }

        [HttpPost]
        [ValidateModel]
        [Route("GetAllProductDatatable")]
        [ProducesResponseType(typeof(DataTablesResponse<GetAllProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProductDatatable([FromForm] DataTablesRequest request)
        {
            var response = await _homeService.GetAllProductDatatable(request);
            return Ok(response);
        }

    }
}
