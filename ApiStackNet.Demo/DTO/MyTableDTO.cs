using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.DTO
{
    public class MyTableDTO : AuditableEntity<Int32>
    {
        public string EntityName { get; set; }
        //other computed or read-only fields here
    }
}