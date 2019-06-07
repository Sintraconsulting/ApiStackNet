using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiStackNet.Demo.Entities
{

    [Table("ORDER", Schema = "DBO")]
    public class Order : AuditableEntity<Int32>
    {
        [Column("ORDER_ID")]
        public int OrderId { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

        [Column("DATA")]
        public DateTime Data { get; set; }

        [Column("AMOUNT")]
        public double Amount { get; set; }


        // --- FK

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}