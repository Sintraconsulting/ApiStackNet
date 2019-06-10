using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.E2DTOMapping
{
    public class UserE2DTOMapping : Profile
    {
        public UserE2DTOMapping()
        {
            CreateMap<User, UserDTO>(); //NO reverse map.ReverseMap();
        }
    }
}