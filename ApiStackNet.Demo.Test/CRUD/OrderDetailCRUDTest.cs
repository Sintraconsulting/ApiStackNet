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

            OrderDetail OrderDetailToCheck = DbContext.OrderDetail.Where(x => x.Id == OrderDetailDTO.Id && x.Active == true).FirstOrDefault();
            var date = DateTime.Now;
            //Check object's fields on SAVE
            Assert.True(OrderDetailToCheck.Id == OrderDetailDTO.Id);
            Assert.True(OrderDetailToCheck.OrderId == OrderDetailDTO.OrderId);
            Assert.True(OrderDetailToCheck.ProductId == OrderDetailDTO.ProductId);
            Assert.True(OrderDetailToCheck.Quantity == OrderDetailDTO.Quantity);
            Assert.True(OrderDetailToCheck.CreatedOn.Date == date.Date);
            Assert.True(OrderDetailToCheck.ModifiedOn.Date == date.Date);

            //New BO
            orderDetailBO.Id = OrderDetailDTO.Id;
            orderDetailBO.Quantity = 3;
            //EDIT         
            OrderDetailService.EditOrderDetail(orderDetailBO);

            OrderDetailToCheck = DbContext.OrderDetail.Where(x => x.Id == orderDetailBO.Id && x.Active == true).FirstOrDefault();
            //Check object's fields on EDIT
            Assert.True(OrderDetailToCheck.Id == orderDetailBO.Id);
            Assert.True(OrderDetailToCheck.OrderId == orderDetailBO.OrderId);
            Assert.True(OrderDetailToCheck.ProductId == orderDetailBO.ProductId);
            Assert.True(OrderDetailToCheck.Quantity == orderDetailBO.Quantity);
            Assert.True(OrderDetailToCheck.CreatedOn == OrderDetailDTO.CreatedOn);
            Assert.True(OrderDetailToCheck.ModifiedOn > OrderDetailDTO.ModifiedOn);
            Assert.True(OrderDetailToCheck.DeletedOn == OrderDetailDTO.DeletedOn);
            Assert.True(OrderDetailToCheck.CreatedBy == OrderDetailDTO.CreatedBy);
            //Assert.True(OrderDetailToCheck.ModifiedBy == OrderDetailDTO.ModifiedBy); who modifies the entity is not always the same who created it
            Assert.True(OrderDetailToCheck.DeletedBy == OrderDetailDTO.DeletedBy);

            //DELETE by Id
            OrderDetailService.Delete(OrderDetailDTO.Id);

            OrderDetailToCheck = DbContext.OrderDetail.Where(x => x.Id == OrderDetailDTO.Id && x.Active == false).FirstOrDefault();
            //Check if entity is not null
            Assert.True(OrderDetailToCheck != null);
            //Check if the deleted date is modified on DELETE
            Assert.True(OrderDetailToCheck.DeletedOn > OrderDetailDTO.DeletedOn);

            //Restore "Old BO" and save the Entity
            orderDetailBO = OrderDetailBOCreate();
            OrderDetailDTO = OrderDetailService.SaveOrderDetail(orderDetailBO);

            //GET BY ID
            OrderDetailDTO = OrderDetailService.GetById(OrderDetailDTO.Id);

            OrderDetailToCheck = DbContext.OrderDetail.Where(x => x.Id == OrderDetailDTO.Id && x.Active == true).FirstOrDefault();
            //Check object's fields on GET BY ID
            Assert.True(OrderDetailDTO.Id == OrderDetailToCheck.Id);
            Assert.True(OrderDetailDTO.OrderId == OrderDetailToCheck.OrderId);
            Assert.True(OrderDetailDTO.ProductId == OrderDetailToCheck.ProductId);
            Assert.True(OrderDetailDTO.Quantity == OrderDetailToCheck.Quantity);
            Assert.True(OrderDetailDTO.CreatedOn == OrderDetailToCheck.CreatedOn);
            Assert.True(OrderDetailDTO.ModifiedOn == OrderDetailToCheck.ModifiedOn);
            Assert.True(OrderDetailDTO.DeletedOn == OrderDetailToCheck.DeletedOn);
            Assert.True(OrderDetailDTO.CreatedBy == OrderDetailToCheck.CreatedBy);
            Assert.True(OrderDetailDTO.ModifiedBy == OrderDetailToCheck.ModifiedBy);
            Assert.True(OrderDetailDTO.DeletedBy == OrderDetailToCheck.DeletedBy);

            //DELETE by Entity
            OrderDetailService.Delete(OrderDetailDTO);

            OrderDetailToCheck = DbContext.OrderDetail.Where(x => x.Id == OrderDetailDTO.Id && x.Active == false).FirstOrDefault();
            //Check if entity is not null
            Assert.True(OrderDetailToCheck != null);
            //Check if the deleted date is modified on DELETE
            Assert.True(OrderDetailToCheck.DeletedOn > OrderDetailDTO.DeletedOn);


            //LIST
            var list = OrderDetailService.GetOrderDetailList();
        }
    }
}