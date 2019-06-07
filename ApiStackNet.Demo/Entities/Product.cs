using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStackNet.Demo.Entities
{

    [Table("PRODUCT", Schema = "DBO")]
    public class Product : AuditableEntity<Int32>
    {
        [Column("PRODUCT_ID")]
        public int ProductId { get; set; }

        [Column("DESCRIPTION")]
        public string Desc { get; set; }

        [Column("PRICE")]
        public double Price { get; set; }

    }
}