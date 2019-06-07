using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BO
{
    public class ProductBO : BaseEntity<Int32>
    {
        public int ProductId { get; set; }

        public string Desc { get; set; }

        public double Price { get; set; }
    }
}