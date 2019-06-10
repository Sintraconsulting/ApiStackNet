using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.Mapping.E2DTOMapping
{
    public class OrderDetailE2DTOMapping : Profile
    {
        public OrderDetailE2DTOMapping()
        {
            CreateMap<OrderDetail, OrderDetailDTO>(); //NO reverse map.ReverseMap();
        }
    }
}