using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BO2EMapping
{
    public class OrderBO2EMapping : Profile
    {
        public OrderBO2EMapping()
        {
            CreateMap<OrderBO, Order>(); //NO reverse map.ReverseMap();
        }
    }
}