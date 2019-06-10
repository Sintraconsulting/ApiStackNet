using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.Mapping.BO2EMapping
{
    public class OrderDetailBO2EMapping : Profile
    {
        public OrderDetailBO2EMapping()
        {
            CreateMap<OrderDetailBO, OrderDetail>(); //NO reverse map.ReverseMap();
        }
    }
}