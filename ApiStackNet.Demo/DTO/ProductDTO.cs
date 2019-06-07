using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApiStackNet.Demo.DTO
{
    public class ProductDTO : AuditableEntity<Int32>
    {
        public int ProductId { get; set; }

        public string Desc { get; set; }

        public double Price { get; set; }
    }
}