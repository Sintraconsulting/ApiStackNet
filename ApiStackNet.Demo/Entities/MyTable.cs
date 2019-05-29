using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStackNet.Demo.Entities
{

    [Table("MYTABLE", Schema = "DBO")]
    public class MyTable : AuditableEntity<Int32>
    {
        [Column("MY_CUSTOM_NAME")]
        public string EntityName { get; set; }
    }
}