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
    public class OrderCRUDTest : BaseTest
    {
        private OrderBO OrderBOCreate()
        {
            OrderBO order = new OrderBO()
            {
                OrderId = 230,
                UserId = 4,
                Data = DateTime.Now,
                Amount = 4
            };

            return order;
        }

        [Fact]
        public void CRUDTest()
        {
            //Old BO
            OrderBO orderBO = OrderBOCreate();

            //SAVE
            OrderDTO OrderDTO = OrderService.SaveOrder(orderBO);

            //New BO
            orderBO.Id = OrderDTO.Id;
            orderBO.Data = DateTime.Now;
            orderBO.Amount = 3;

            //EDIT         
            OrderService.EditOrder(orderBO);

            //DELETE by Id
            OrderService.Delete(OrderDTO.Id);

            //Restore "Old BO" and save the Entity
            orderBO = OrderBOCreate();
            OrderDTO = OrderService.SaveOrder(orderBO);

            //GET BY ID
            OrderDTO = OrderService.GetById(OrderDTO.Id);

            //DELETE by Entity
            OrderService.Delete(OrderDTO);

            //LIST
            var list = OrderService.GetOrdersList();
        }
    }
}