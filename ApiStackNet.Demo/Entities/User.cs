using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStackNet.Demo.Entities
{
    [Table("USER", Schema = "DBO")]
    public class User : AuditableEntity<Int32>
    {
        [Column("USER_ID")]
        public int UserId { get; set; }

        [Column("ORDER_ID")]
        public int OrderId { get; set; }

        [Column("PRODUCT_ID")]
        public int ProductId { get; set; }

        [Column("QUANTITY")]
        public double Quantity { get; set; }


        // --- FK

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}