using ApiStackNet.BLL.Service;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BLL.Services
{
    public class ProductService : IntDataService<ProductDTO, ProductBO, Product>
    {
        public override IQueryable<Product> GetQueriable()
        {
            return base.GetQueriable().Where(x => x.Active == true);
        }

        public ProductService(DbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public ProductDTO SaveProduct(ProductBO productBO)
        {
            ProductDTO productDTO = base.Save(productBO);

            return productDTO;
        }

        public List<ProductDTO> GetProductsList()
        {
            List<ProductDTO> productsList = new List<ProductDTO>();

            var products = this.GetQueriable().ToList();

            foreach (Product product in products)
            {
                ProductDTO productDTO = mapper.Map<Product, ProductDTO>(product);
                productsList.Add(productDTO);
            }

            return productsList;
        }
    }
}