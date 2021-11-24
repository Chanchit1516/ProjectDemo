using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo.Core.DTOs
{
    public class BaseDto
    {
        public DateTime CreatedDateTime { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
