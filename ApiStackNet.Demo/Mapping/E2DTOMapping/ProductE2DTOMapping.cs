using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.E2DTOMapping
{
    public class ProductE2DTOMapping : Profile
    {
        public ProductE2DTOMapping()
        {
            CreateMap<Product, ProductDTO>(); //NO reverse map.ReverseMap();
        }
    }
}