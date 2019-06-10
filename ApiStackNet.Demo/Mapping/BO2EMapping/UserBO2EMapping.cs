using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BO2EMapping
{
    public class UserBO2EMapping : Profile
    {
        public UserBO2EMapping()
        {
            CreateMap<UserBO, User>(); //NO reverse map.ReverseMap();
        }
    }
}