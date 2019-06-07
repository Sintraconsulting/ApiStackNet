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

        [Column("NAME")]
        public string Name { get; set; }

        [Column("ADDRESS")]
        public string Address { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }
    }
}