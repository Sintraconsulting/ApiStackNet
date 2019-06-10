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
    public class OrderDetailController : DataController<OrderDetailService, OrderDetailDTO, OrderDetailBO, OrderDetail, int>
    {
        private OrderDetailService OrderDetailService;

        public OrderDetailController(OrderDetailService service) : base(service)
        {
            this.OrderDetailService = service;
        }

        [HttpGet]
        [Route("{id}")]
        public WrappedResponse<OrderDetailDTO> GetById([FromUri] int id)
        {
            return WrappedOK(this.OrderDetailService.GetById(id));
        }

        [HttpGet]
        [Route("order_detail-list")]
        public WrappedResponse<List<OrderDetailDTO>> GetUsersList()
        {
            return WrappedOK(this.OrderDetailService.GetProductsList());
        }

        [HttpPost]
        [Route("save")]
        public WrappedResponse<OrderDetailDTO> SaveProduct(OrderDetailBO orderDetailBO)
        {
            return WrappedOK(this.OrderDetailService.SaveOrderDetail(orderDetailBO));
        }

        [HttpDelete]
        [Route("delete")]
        public override WrappedResponse<bool> Delete(int Id)
        {
            return base.Delete(Id);
        }

        [HttpDelete]
        [Route("delete")]
        public WrappedResponse<bool> DeleteUserByEntity(OrderDetail orderDetail)
        {
            return base.Delete(orderDetail.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public WrappedResponse<OrderDetailDTO> EditProduct(OrderDetailBO orderDetailBO)
        {
            return WrappedOK(this.OrderDetailService.EditOrderDetail(orderDetailBO));
        }
    }
}