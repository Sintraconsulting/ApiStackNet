using ApiStackNet.BLL.Service;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BLL.Services
{
    public class OrderDetailService : IntDataService<OrderDetailDTO, OrderDetailBO, OrderDetail>
    {
        public override IQueryable<OrderDetail> GetQueriable()
        {
            return base.GetQueriable().Where(x => x.Active == true);
        }

        public OrderDetailService(DbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}