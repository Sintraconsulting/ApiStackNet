using ApiStackNet.API.Controllers;
using ApiStackNet.BLL.Service;
using ApiStackNet.Demo.BLL.Services;
using ApiStackNet.Demo.Controllers.Api;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;

namespace ApiStackNet.Demo.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterContainer(HttpConfiguration config)
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            
            builder.RegisterType<ApiStackNetDBContext>().As<DbContext>().InstancePerRequest();
            builder.RegisterType<OrderDataService>().AsSelf().InstancePerRequest();
            builder.RegisterType<MessageService>().AsSelf().InstancePerRequest();
            builder.RegisterModule(new MapperInstaller());

            var container = builder.Build();

            Mapper.Initialize(x => x.ConstructServicesUsing(container.Resolve));

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}