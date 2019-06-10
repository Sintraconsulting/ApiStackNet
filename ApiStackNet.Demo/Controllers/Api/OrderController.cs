using ApiStackNet.API.Controllers;
using ApiStackNet.API.Model;
using ApiStackNet.Demo.BLL.Services;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiStackNet.Demo.Controllers.Api
{
    [RoutePrefix("api/order")]
    public class OrderController : DataController<OrderService, OrderDTO, OrderBO, Order, int>
    {
        private OrderService OrderService;

        public OrderController(OrderService service) : base(service)
        {
            this.OrderService = service;
        }

        [HttpGet]
        [Route("{id}")]
        public WrappedResponse<OrderDTO> GetById([FromUri] int id)
        {
            return WrappedOK(this.OrderService.GetById(id));
        }

        [HttpPost]
        [Route("save")]
        public WrappedResponse<OrderDTO> SaveOrder(OrderBO myTableBO)
        {
            return WrappedOK(this.OrderService.SaveOrder(myTableBO));
        }

        [HttpGet]
        [Route("order-list")]
        public WrappedResponse<List<OrderDTO>> GetOrdersList()
        {
            return WrappedOK(this.OrderService.GetOrdersList());
        }
    }
}