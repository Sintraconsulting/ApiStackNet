using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStackNet.Demo.Entities
{

    [Table("ORDER_DETAIL", Schema = "DBO")]
    public class OrderDetail : AuditableEntity<Int32>
    {
        [Column("ORDER_ID")]
        public int OrderId { get; set; }
        
        [Column("PRODUCT_ID")]
        public int ProductId { get; set; }

        [Column("QUANTITY")]
        public int Quantity { get; set; }


        // --- FK

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}