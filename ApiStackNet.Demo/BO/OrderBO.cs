﻿using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BO
{
    public class OrderBO : BaseEntity<Int32>
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string OrderDesc { get; set; }

        public double Price { get; set; }

        public string Address { get; set; }
    }
}