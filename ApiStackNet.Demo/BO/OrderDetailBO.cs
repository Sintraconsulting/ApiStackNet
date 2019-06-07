using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BO
{
    public class OrderDetailBO : BaseEntity<Int32>
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}