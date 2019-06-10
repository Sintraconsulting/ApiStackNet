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
    [RoutePrefix("api/product")]
    public class ProductController : DataController<ProductService, ProductDTO, ProductBO, Product, int>
    {
        private ProductService ProductService;

        public ProductController(ProductService service) : base(service)
        {
            this.ProductService = service;
        }

        [HttpGet]
        [Route("{id}")]
        public WrappedResponse<ProductDTO> GetById([FromUri] int id)
        {
            return WrappedOK(this.ProductService.GetById(id));
        }

        [HttpGet]
        [Route("product-list")]
        public WrappedResponse<List<ProductDTO>> GetProductsList()
        {
            return WrappedOK(this.ProductService.GetProductsList());
        }

        [HttpPost]
        [Route("save")]
        public WrappedResponse<ProductDTO> SaveProduct(ProductBO productBO)
        {
            return WrappedOK(this.ProductService.SaveProduct(productBO));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public override WrappedResponse<bool> Delete(int Id)
        {
            return base.Delete(Id);
        }

        [HttpDelete]
        [Route("delete")]
        public WrappedResponse<bool> DeleteProductByEntity(Product product)
        {
            return base.Delete(product.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public WrappedResponse<ProductDTO> EditProduct(ProductBO productBO)
        {
            return WrappedOK(this.ProductService.EditProduct(productBO));
        }
    }
}