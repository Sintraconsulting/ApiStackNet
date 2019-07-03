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
                OrderId = 230, //to check
                UserId = 4, //to check
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

            Order OrderToCheck = DbContext.Order.Where(x => x.Id == OrderDTO.Id && x.Active == true).FirstOrDefault();
            var date = DateTime.Now;
            //Check object's fields on SAVE
            Assert.True(OrderToCheck.Id == OrderDTO.Id);
            Assert.True(OrderToCheck.OrderId == OrderDTO.OrderId);
            Assert.True(OrderToCheck.UserId == OrderDTO.UserId);
            Assert.True(OrderToCheck.Data == OrderDTO.Data);
            Assert.True(OrderToCheck.Amount == OrderDTO.Amount);
            Assert.True(OrderToCheck.CreatedOn.Date == date.Date);
            Assert.True(OrderToCheck.ModifiedOn.Date == date.Date);

            //New BO
            orderBO.Id = OrderDTO.Id;
            orderBO.Data = DateTime.Now;
            orderBO.Amount = 3;

            //EDIT         
            OrderService.EditOrder(orderBO);

            OrderToCheck = DbContext.Order.Where(x => x.Id == orderBO.Id && x.Active == true).FirstOrDefault();
            //Check object's fields on EDIT
            Assert.True(OrderToCheck.Id == orderBO.Id);
            Assert.True(OrderToCheck.OrderId == orderBO.OrderId);
            Assert.True(OrderToCheck.UserId == orderBO.UserId);
            Assert.True(OrderToCheck.Data == orderBO.Data);
            Assert.True(OrderToCheck.Amount == orderBO.Amount);
            Assert.True(OrderToCheck.CreatedOn == OrderDTO.CreatedOn);
            Assert.True(OrderToCheck.ModifiedOn > OrderDTO.ModifiedOn);
            Assert.True(OrderToCheck.DeletedOn == OrderDTO.DeletedOn);
            Assert.True(OrderToCheck.CreatedBy == OrderDTO.CreatedBy);
            //Assert.True(OrderToCheck.ModifiedBy == OrderDTO.ModifiedBy); who modifies the entity is not always the same who created it
            Assert.True(OrderToCheck.DeletedBy == OrderDTO.DeletedBy);

            //DELETE by Id
            OrderService.Delete(OrderDTO.Id);

            OrderToCheck = DbContext.Order.Where(x => x.Id == OrderDTO.Id && x.Active == false).FirstOrDefault();
            //Check if entity is not null
            Assert.True(OrderToCheck != null);
            //Check if the deleted date is modified on DELETE
            Assert.True(OrderToCheck.DeletedOn > OrderDTO.DeletedOn);

            //Restore "Old BO" and save the Entity
            orderBO = OrderBOCreate();
            OrderDTO = OrderService.SaveOrder(orderBO);

            //GET BY ID
            OrderDTO = OrderService.GetById(OrderDTO.Id);

            OrderToCheck = DbContext.Order.Where(x => x.Id == OrderDTO.Id && x.Active == true).FirstOrDefault();
            //Check object's fields on GET BY ID
            Assert.True(OrderDTO.Id == OrderToCheck.Id);
            Assert.True(OrderDTO.OrderId == OrderToCheck.OrderId);
            Assert.True(OrderDTO.UserId == OrderToCheck.UserId);
            Assert.True(OrderDTO.Data == OrderToCheck.Data);
            Assert.True(OrderToCheck.Amount == orderBO.Amount);
            Assert.True(OrderDTO.CreatedOn == OrderToCheck.CreatedOn);
            Assert.True(OrderDTO.ModifiedOn == OrderToCheck.ModifiedOn);
            Assert.True(OrderDTO.DeletedOn == OrderToCheck.DeletedOn);
            Assert.True(OrderDTO.CreatedBy == OrderToCheck.CreatedBy);
            Assert.True(OrderDTO.ModifiedBy == OrderToCheck.ModifiedBy);
            Assert.True(OrderDTO.DeletedBy == OrderToCheck.DeletedBy);

            //DELETE by Entity
            OrderService.Delete(OrderDTO);

            OrderToCheck = DbContext.Order.Where(x => x.Id == OrderDTO.Id && x.Active == false).FirstOrDefault();
            //Check if entity is not null
            Assert.True(OrderToCheck != null);
            //Check if the deleted date is modified on DELETE
            Assert.True(OrderToCheck.DeletedOn > OrderDTO.DeletedOn);

            //LIST
            var list = OrderService.GetOrdersList();
        }
    }
}