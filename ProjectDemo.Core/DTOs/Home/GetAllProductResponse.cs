using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo.Core.DTOs.Home
{
    public class GetAllProductResponse : BaseDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
