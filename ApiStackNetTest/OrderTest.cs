using System;
using Xunit;
using ApiStackNet.Demo;
using ApiStackNet.Demo.Entities;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.BLL.Services;
using ApiStackNet.Demo.DTO;
using System.Collections.Generic;

namespace ApiStackNetXUnitTest
{
    public class OrderTest: BaseTest
    {
        [Fact]
        public void GetOrderByIdTest()
        {
            int i = 1;

            try
            {
                Order order = this.DbContext.Order.Find(i);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Fact]
        public void SaveOrderTest()
        {
            OrderBO orderBO = new OrderBO()
            {
                UserId = 1,
                UserName = "Chiara",
                Email = "c.bernardini@sintraconsulting.eu",
                OrderDesc = "T-Shirt",
                Price = 20.00,
                Address = "Via Fratelli Lumiere, 19, 52100 Arezzo AR"
            };


            try
            {
                OrderDTO orderDTO = OrderDataService.SaveOrder(orderBO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Fact]
        public void GetOrdersListTest()
        {
            try
            {
                List<OrderDTO> ordersDTOList = OrderDataService.GetOrdersList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Fact]
        public void CRUDTest()
        {
            //SAVE
            OrderBO orderBO = new OrderBO()
            {
                UserId = 1,
                UserName = "Chiara",
                Email = "c.bernardini@sintraconsulting.eu",
                OrderDesc = "T-Shirt",
                Price = 20.00,
                Address = "Via Fratelli Lumiere, 19, 52100 Arezzo AR"
            };

            OrderDTO newOrderDTO = OrderDataService.Save(orderBO);


            //DELETE FROM ID
            bool orderDeletedFromId = OrderDataService.Delete(newOrderDTO.Id);

            //SAVE
            OrderBO orderBO2 = new OrderBO()
            {
                UserId = 1,
                UserName = "Chiara",
                Email = "c.bernardini@sintraconsulting.eu",
                OrderDesc = "T-Shirt",
                Price = 20.00,
                Address = "Via Fratelli Lumiere, 19, 52100 Arezzo AR"
            };

            OrderDTO newOrderDTO2 = OrderDataService.Save(orderBO2);

            //DELETE FROM DTO
            bool orderDeletedFromDTO = OrderDataService.Delete(newOrderDTO2);

        }
    }
}