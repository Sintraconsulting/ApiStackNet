using System;
using Xunit;
using ApiStackNet.Demo;
using ApiStackNet.Demo.Entities;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.BLL.Services;
using ApiStackNet.Demo.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ApiStackNet.Demo.Test.CRUD
{
    public class OrderDetailCRUDTest : BaseTest
    {
        private OrderDetailBO OrderDetailBOCreate()
        {
            OrderDetailBO orderDetail = new OrderDetailBO()
            {
                OrderId = 45, //to check
                ProductId = 1002, //to check
                Quantity = 2
            };

            return orderDetail;
        }

        [Fact]
        public void CRUDTest()
        {
            //Old BO
            OrderDetailBO orderDetailBO = OrderDetailBOCreate();

            //SAVE
            OrderDetailDTO OrderDetailDTO = OrderDetailService.SaveOrderDetail(orderDetailBO);

            //New BO
            orderDetailBO.Id = OrderDetailDTO.Id;
            orderDetailBO.Quantity = 3;
            //EDIT         
            OrderDetailService.EditOrderDetail(orderDetailBO);

            //DELETE by Id
            OrderDetailService.Delete(OrderDetailDTO.Id);

            //Restore "Old BO" and save the Entity
            orderDetailBO = OrderDetailBOCreate();
            OrderDetailDTO = OrderDetailService.SaveOrderDetail(orderDetailBO);

            //GET BY ID
            OrderDetailDTO = OrderDetailService.GetById(OrderDetailDTO.Id);

            //DELETE by Entity
            OrderDetailService.Delete(OrderDetailDTO);

            //LIST
            var list = OrderDetailService.GetOrderDetailList();
        }
    }
}