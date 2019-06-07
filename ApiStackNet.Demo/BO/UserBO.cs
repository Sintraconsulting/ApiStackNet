using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BO
{
    public class UserBO : BaseEntity<Int32>
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public int Address { get; set; }

        public string Email { get; set; }
    }
}