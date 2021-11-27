using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo.Core.DTOs
{
    public class DataTablesRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<ColumnDT> Columns { get; set; }
        public Search Search { get; set; }
        public List<Order> Order { get; set; }
        public List<Filter> Filters { get; set; }
    }

    public class ColumnDT
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }
    }

    public class Search
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }

    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class Filter
    {
        public string Column { get; set; }
        public string Value { get; set; }
    }
}
