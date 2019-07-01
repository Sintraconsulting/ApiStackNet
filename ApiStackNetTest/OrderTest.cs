//using System;
//using Xunit;
//using ApiStackNet.Demo;
//using ApiStackNet.Demo.Entities;
//using ApiStackNet.Demo.BO;
//using ApiStackNet.Demo.BLL.Services;
//using ApiStackNet.Demo.DTO;
//using System.Collections.Generic;

//namespace ApiStackNetXUnitTest
//{
//    public class OrderTest: BaseTest
//    {
//        [Fact]
//        public void GetOrderByIdTest()
//        {
//            int i = 1;
//            bool check = false;

//            try
//            {
//                Order order = this.DbContext.Order.Find(i);
//                if (order != null)
//                {
//                    check = true;
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.Message);
//            }

//            Assert.True(check);
//        }

//        [Fact]
//        public void SaveOrderTest()
//        {
//            bool check = false;

//            OrderBO orderBO = new OrderBO()
//            {
//                UserId = 1,
//                UserName = "Chiara",
//                Email = "c.bernardini@sintraconsulting.eu",
//                OrderDesc = "T-Shirt",
//                Price = 20.00,
//                Address = "Via Fratelli Lumiere, 19, 52100 Arezzo AR"
//            };


//            try
//            {
//                OrderDTO orderDTO = OrderDataService.SaveOrder(orderBO);
//                if (orderDTO != null)
//                {
//                    check = true;
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.Message);
//            }

//            Assert.True(check);
//        }

//        [Fact]
//        public void GetOrdersListTest()
//        {
//            bool check = false;

//            try
//            {
//                List<OrderDTO> ordersDTOList = OrderDataService.GetOrdersList();
//                if (ordersDTOList != null)
//                {
//                    check = true;
//                }

//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.Message);
//            }

//            Assert.True(check);
//        }

//TODO: Rendere specifici i singoli test

//[Fact]
//public void CRUDTest()
//{
//    //SAVE
//    OrderBO orderBO = new OrderBO()
//    {
//        UserId = 1,
//        UserName = "Chiara",
//        Email = "c.bernardini@sintraconsulting.eu",
//        OrderDesc = "T-Shirt",
//        Price = 20.00,
//        Address = "Via Fratelli Lumiere, 19, 52100 Arezzo AR"
//    };

//    OrderDTO newOrderDTO = OrderDataService.Save(orderBO);


//    //DELETE FROM ID
//    bool orderDeletedFromId = OrderDataService.Delete(newOrderDTO.Id);

//    //SAVE
//    OrderBO orderBO2 = new OrderBO()
//    {
//        UserId = 1,
//        UserName = "Chiara",
//        Email = "c.bernardini@sintraconsulting.eu",
//        OrderDesc = "T-Shirt",
//        Price = 20.00,
//        Address = "Via Fratelli Lumiere, 19, 52100 Arezzo AR"
//    };

//    OrderDTO newOrderDTO2 = OrderDataService.Save(orderBO2);

//    //DELETE FROM DTO
//    bool orderDeletedFromDTO = OrderDataService.Delete(newOrderDTO2);

//}
////    }
////}