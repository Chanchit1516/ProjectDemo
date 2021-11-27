using AutoMapper;
using ProjectDemo.Core.DTOs.Home;
using ProjectDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo.Application.Mappers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Product, GetAllProductResponse>();
            CreateMap<AddProductTestRequest, Product>();
        }
    }
}
