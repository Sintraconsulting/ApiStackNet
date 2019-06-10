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

        [HttpGet]
        [Route("order-list")]
        public WrappedResponse<List<OrderDTO>> GetOrdersList()
        {
            return WrappedOK(this.OrderService.GetOrdersList());
        }

        [HttpPost]
        [Route("save")]
        public WrappedResponse<OrderDTO> SaveOrder(OrderBO orderBO)
        {
            return WrappedOK(this.OrderService.SaveOrder(orderBO));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public override WrappedResponse<bool> Delete(int Id)
        {
            return base.Delete(Id);
        }

        [HttpDelete]
        [Route("delete")]
        public WrappedResponse<bool> DeleteOrderByEntity(Order order)
        {
            return base.Delete(order.Id);
        }

        [HttpPut]
        [Route("edit/{id}")]
        public WrappedResponse<OrderDTO> EditOrder(OrderBO orderBO)
        {
            return WrappedOK(this.OrderService.EditOrder(orderBO));
        }
    }
}