﻿using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.DTO
{
    public class OrderDetailDTO : AuditableEntity<Int32>
    {
        public int OrderId { get; set; }


        public int ProductId { get; set; }


        public int Quantity { get; set; }
    }
}