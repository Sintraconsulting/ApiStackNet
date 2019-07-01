using System;
using Xunit;
using ApiStackNet.Demo;
using ApiStackNet.Demo.Entities;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.BLL.Services;
using ApiStackNet.Demo.DTO;
using System.Collections.Generic;
using ApiStackNetXUnitTest;
using System.Linq;
using System.Web.Http;

namespace ApiStackNetDemoTest
{
    public class ProductCRUDTest : BaseTest
    {
        private ProductBO ProductBOCreate()
        {
            ProductBO product = new ProductBO()
            {
                ProductId = 001,
                Desc = "Product test",
                Price = 23.99
            };

            return product;
        }

        [Fact]
        public void CRUDTest()
        {
            //Old BO
            ProductBO productBO = ProductBOCreate();

            //SAVE
            ProductDTO ProductDTO = ProductService.SaveProduct(productBO);

            //New BO
            productBO.Id = ProductDTO.Id;
            productBO.Desc = "Product test modified";

            //EDIT         
            ProductService.EditProduct(productBO);

            //DELETE by Id
            ProductService.Delete(ProductDTO.Id);

            //Restore "Old BO" and save the Entity
            productBO = ProductBOCreate();
            ProductDTO = ProductService.SaveProduct(productBO);

            //GET BY ID
            ProductDTO = ProductService.GetById(ProductDTO.Id);

            //DELETE by Entity
            ProductService.Delete(ProductDTO);

            //LIST
            var list = ProductService.GetProductsList();
        }
    }
}
