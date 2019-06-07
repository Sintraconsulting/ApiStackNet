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

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}