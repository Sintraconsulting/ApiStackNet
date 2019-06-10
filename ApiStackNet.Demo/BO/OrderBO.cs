using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BO
{
    public class OrderBO : BaseEntity<Int32>
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime Data { get; set; }

        public double Amount { get; set; }
    }
}