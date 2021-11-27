using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo.Core.DTOs
{
    public class DataTablesResponse<T>
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable<T> Data { get; set; }
        public string Error { get; set; }

        public DataTablesResponse()
        {
            Data = new List<T>();
        }
    }
}
