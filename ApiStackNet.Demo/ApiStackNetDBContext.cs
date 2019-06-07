using ApiStackNet.DAL;
using ApiStackNet.Demo.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo
{
    public class ApiStackNetDBContext:ApiStackDbContext
    {
        public ApiStackNetDBContext() : base("ApiStackNetDatabase")
        {
            Database.SetInitializer<ApiStackNetDBContext>(null);
            this.Configuration.LazyLoadingEnabled = false;

            EnableVerboseSQLog();
        }

        //entities
        public DbSet<User> User { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<OrderDetail> OrderDetail { get; set; }
    }
}