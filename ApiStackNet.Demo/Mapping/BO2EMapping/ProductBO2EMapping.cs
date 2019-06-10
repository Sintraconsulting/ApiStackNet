using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BO2EMapping
{
    public class ProductBO2EMapping : Profile
    {
        public ProductBO2EMapping()
        {
            CreateMap<ProductBO, Product>(); //NO reverse map.ReverseMap();
        }
    }
}