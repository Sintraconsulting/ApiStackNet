using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BO
{
    public class MyTableBO: BaseEntity<Int32>
    {
        public string EntityName { get; set; }
        //no computed fields here
        //no read only fields
    }
}