using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ApiStackNet.Demo.DTO
{
    public class UserDTO : AuditableEntity<Int32>
    {
        public int UserId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public double Quantity { get; set; }
    }
}