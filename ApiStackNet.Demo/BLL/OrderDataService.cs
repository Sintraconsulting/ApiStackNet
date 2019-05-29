﻿using ApiStackNet.BLL.Service;
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
    public class OrderDataService : IntDataService<OrderDTO, OrderBO, Order>
    {
        public override IQueryable<Order> GetQueriable()
        {
            return base.GetQueriable().Where(x => x.Active == true);
        }

        public OrderDataService(DbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public OrderDTO SaveOrder(OrderBO orderBO)
        {
            OrderDTO orderDTO = base.Save(orderBO);

            return orderDTO;
        }

        public List<OrderDTO> GetOrdersList()
        {
            List<OrderDTO> ordersList = new List<OrderDTO>();

            var orders = this.GetQueriable().ToList();

            foreach(Order order in orders)
            {
                OrderDTO orderDTO = mapper.Map<Order, OrderDTO>(order);
                ordersList.Add(orderDTO);
            }

            return ordersList;
        }
    }
}