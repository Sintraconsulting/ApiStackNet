using ApiStackNet.Demo;
using ApiStackNet.Demo.BLL;
using Autofac;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.WebApi;
using System.Reflection;
using Autofac.Integration.Mvc;
using System.Data.Entity;
using AutoMapper;
using ApiStackNet.Demo.BLL.Services;

namespace ApiStackNetXUnitTest
{
    public class BaseTest
    {
        public BaseTest()
        {
            InjectContext();
        }
        public Logger Logger { get; set; } = LogManager.GetCurrentClassLogger();

        public ApiStackNetDBContext DbContext { get; set; }

        public IContainer Container { get; set; }

        public OrderService OrderService { get; set; }
        public OrderDetailService OrderDetailService { get; set; }
        public ProductService ProductService { get; set; }
        public UserService UserService { get; set; }

        protected void InjectContext()
        {
            try
            {
                Logger.Debug("======> Injecting context for test initialization");

                // Create your builder.
                var builder = new ContainerBuilder();

                // register all profile classes in the calling assembly
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();//.Where(x => x.FullName.StartsWith("Prada.")).ToArray(); //DANIELE: This limit search to application context and sppedup startup
                builder.RegisterAssemblyTypes(assemblies)
                    .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                    .As<Profile>();

                builder.Register(c => new MapperConfiguration(cfg =>
                {
                    foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                    {
                        cfg.AddProfile(profile);
                    }
                })).AsSelf().SingleInstance();

                builder.Register(c => c.Resolve<MapperConfiguration>()
                    .CreateMapper(c.Resolve))
                    .As<IMapper>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<ApiStackNetDBContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
                builder.RegisterType<OrderService>().As<OrderService>().InstancePerLifetimeScope();
                builder.RegisterType<OrderDetailService>().As<OrderDetailService>().InstancePerLifetimeScope();
                builder.RegisterType<ProductService>().As<ProductService>().InstancePerLifetimeScope();
                builder.RegisterType<UserService>().As<UserService>().InstancePerLifetimeScope();

                builder.RegisterModule(new MapperInstaller());

                var container = builder.Build();

                Container = container;

                OrderService = container.Resolve<OrderService>();
                OrderDetailService = container.Resolve<OrderDetailService>();
                ProductService = container.Resolve<ProductService>();
                UserService = container.Resolve<UserService>();

                this.DbContext = container.Resolve<ApiStackNetDBContext>();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
            finally
            {
                Logger.Debug("Injecting context for test initialization <======");
            }
        }
    }

}