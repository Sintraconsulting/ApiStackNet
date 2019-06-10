using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.E2DTOMapping
{
    public class OrderE2DTOMapping : Profile
    {
        public OrderE2DTOMapping()
        {
            CreateMap<Order, OrderDTO>(); //NO reverse map.ReverseMap();
        }
    }
}