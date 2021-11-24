using Microsoft.AspNetCore.Mvc;
using ProjectDemo.Api.Filters;
using ProjectDemo.Application.Extensions;
using ProjectDemo.Application.Helpers;
using ProjectDemo.Core.DTOs.Home;
using ProjectDemo.Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;

        }

        [HttpPost]
        [ValidateModel]
        [Route("AddProductTest")]
        public async Task<IActionResult> AddProductTest([FromBody] AddProductTestRequest request)
        {
            var response = await _homeService.AddProductTest(request);
            return Ok(response);
        }

        [HttpGet]
        [ValidateModel]
        [Route("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var response = await _homeService.GetAllProduct();
            return Ok(response);
        }

    }
}
