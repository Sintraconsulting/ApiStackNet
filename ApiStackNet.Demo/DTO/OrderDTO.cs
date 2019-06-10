using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.DTO
{
    public class OrderDTO : AuditableEntity<Int32>
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime Data { get; set; }

        public double Amount { get; set; }
    }
}