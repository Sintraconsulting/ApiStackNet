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

            Product ProductToCheck = DbContext.Product.Where(x => x.Id == ProductDTO.Id && x.Active == true).FirstOrDefault();
            var date = DateTime.Now;
            //Check object's fields on SAVE
            Assert.True(ProductToCheck.Id == ProductDTO.Id);
            Assert.True(ProductToCheck.ProductId == ProductDTO.ProductId);
            Assert.True(ProductToCheck.Desc == ProductDTO.Desc);
            Assert.True(ProductToCheck.Price == ProductDTO.Price);
            Assert.True(ProductToCheck.CreatedOn.Date == date.Date);
            Assert.True(ProductToCheck.ModifiedOn.Date == date.Date);

            //New BO
            productBO.Id = ProductDTO.Id;
            productBO.Desc = "Product test modified";

            //EDIT         
            ProductService.EditProduct(productBO);

            ProductToCheck = DbContext.Product.Where(x => x.Id == productBO.Id && x.Active == true).FirstOrDefault();
            //Check object's fields on EDIT
            Assert.True(ProductToCheck.Id == productBO.Id);
            Assert.True(ProductToCheck.ProductId == productBO.ProductId);
            Assert.True(ProductToCheck.Desc == productBO.Desc);
            Assert.True(ProductToCheck.Price == productBO.Price);
            Assert.True(ProductToCheck.CreatedOn == ProductDTO.CreatedOn);
            Assert.True(ProductToCheck.ModifiedOn > ProductDTO.ModifiedOn);
            Assert.True(ProductToCheck.DeletedOn == ProductDTO.DeletedOn);
            Assert.True(ProductToCheck.CreatedBy == ProductDTO.CreatedBy);
            //Assert.True(ProductToCheck.ModifiedBy == ProductDTO.ModifiedBy); who modifies the entity is not always the same who created it
            Assert.True(ProductToCheck.DeletedBy == ProductDTO.DeletedBy);

            //DELETE by Id
            ProductService.Delete(ProductDTO.Id);

            ProductToCheck = DbContext.Product.Where(x => x.Id == ProductDTO.Id && x.Active == false).FirstOrDefault();
            //Check if entity is not null
            Assert.True(ProductToCheck != null);
            //Check if the deleted date is modified on DELETE
            Assert.True(ProductToCheck.DeletedOn > ProductDTO.DeletedOn);


            //Restore "Old BO" and save the Entity
            productBO = ProductBOCreate();
            ProductDTO = ProductService.SaveProduct(productBO);

            //GET BY ID
            ProductDTO = ProductService.GetById(ProductDTO.Id);

            ProductToCheck = DbContext.Product.Where(x => x.Id == ProductDTO.Id && x.Active == true).FirstOrDefault();
            //Check object's fields on GET BY ID
            Assert.True(ProductDTO.Id == ProductToCheck.Id);
            Assert.True(ProductDTO.ProductId == ProductToCheck.ProductId);
            Assert.True(ProductDTO.Desc == ProductToCheck.Desc);
            Assert.True(ProductDTO.Price == ProductToCheck.Price);
            Assert.True(ProductDTO.CreatedOn == ProductToCheck.CreatedOn);
            Assert.True(ProductDTO.ModifiedOn == ProductToCheck.ModifiedOn);
            Assert.True(ProductDTO.DeletedOn == ProductToCheck.DeletedOn);
            Assert.True(ProductDTO.CreatedBy == ProductToCheck.CreatedBy);
            Assert.True(ProductDTO.ModifiedBy == ProductToCheck.ModifiedBy);
            Assert.True(ProductDTO.DeletedBy == ProductToCheck.DeletedBy);

            //DELETE by Entity
            ProductService.Delete(ProductDTO);

            ProductToCheck = DbContext.Product.Where(x => x.Id == ProductDTO.Id && x.Active == false).FirstOrDefault();
            //Check if entity is not null
            Assert.True(ProductToCheck != null);
            //Check if the deleted date is modified on DELETE
            Assert.True(ProductToCheck.DeletedOn > ProductDTO.DeletedOn);

            //LIST
            var list = ProductService.GetProductsList();
        }
    }
}
