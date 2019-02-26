using ApiStackNet.DAL.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.DAL
{
    public class ApiStackDbContext:DbContext
    {

        public  Logger Logger { get; private set; } = LogManager.GetCurrentClassLogger();



        public ApiStackDbContext(string nameOrConnectionString): base(nameOrConnectionString) { }

        public ApiStackDbContext(string nameOrConnectionString, DbCompiledModel model):base(nameOrConnectionString, model) { }
        public ApiStackDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) { }
        public ApiStackDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext) : base(objectContext,dbContextOwnsObjectContext) { }
        public ApiStackDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection) { }
        public ApiStackDbContext() : base() { }
        public ApiStackDbContext(DbCompiledModel model) : base(model) { }


        public virtual void EnableVerboseSQLog()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["ApiStackNet:EnableVerboseSQLOperation"]))
            {
                this.Database.Log = s => this.Logger.Debug(s);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(config => config.HasPrecision(18, 6));
        }


        public void SetValueOnAuditableEntity(object subject, string property, object value)
        {
            Type AuditableEntityType = typeof(AuditableEntity<>);
            Type typeWithPK = AuditableEntityType.MakeGenericType(subject.GetType().BaseType.GenericTypeArguments[0]);
            var pp = typeWithPK.GetProperty(property);
            pp.GetSetMethod().Invoke(subject, new object[] { value });
        }


        public override int SaveChanges(
            )
        {

            foreach (var item in this.ChangeTracker.Entries())
            {

                if (item.Entity != null && item.Entity.GetType().BaseType.Name == "AuditableEntity`1")//TODO: make generic
                {
                    if (item.State == EntityState.Added)
                    {
                        SetValueOnAuditableEntity(item.Entity, "Active", true);
                        SetValueOnAuditableEntity(item.Entity, "CreatedOn", DateTime.Now);
                        if (!string.IsNullOrEmpty(ClaimsPrincipal.Current.Identity.Name))
                        {
                            SetValueOnAuditableEntity(item.Entity, "CreatedBy", ClaimsPrincipal.Current.Identity.Name);
                        }
                    }

                    if (item.State == EntityState.Added || item.State == EntityState.Modified)
                    {
                        SetValueOnAuditableEntity(item.Entity, "ModifiedOn", DateTime.Now);
                        if (!string.IsNullOrEmpty(ClaimsPrincipal.Current.Identity.Name))
                        {
                            SetValueOnAuditableEntity(item.Entity, "ModifiedBy", ClaimsPrincipal.Current.Identity.Name);
                        }
                    }


                    if (item.State == EntityState.Deleted)
                    {
                        item.State = EntityState.Modified;
                        SetValueOnAuditableEntity(item.Entity, "Active", false);
                        SetValueOnAuditableEntity(item.Entity, "DeletedOn", DateTime.Now);
                        if (!string.IsNullOrEmpty(ClaimsPrincipal.Current.Identity.Name))
                        {
                            SetValueOnAuditableEntity(item.Entity, "DeletedBy", ClaimsPrincipal.Current.Identity.Name);
                        }
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
