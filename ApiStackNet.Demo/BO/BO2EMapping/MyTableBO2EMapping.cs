using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BO2EMapping
{
    public class MyTableBO2EMapping : Profile
    {
        public MyTableBO2EMapping()
        {
            CreateMap<MyTableBO, MyTable>(); //NO reverse map.ReverseMap();
        }
    }

}