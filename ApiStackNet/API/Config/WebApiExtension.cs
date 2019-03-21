using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace ApiStackNet.API.Config
{
    public static class WebApiExtension
    {
        public static void ApiStackMapHttpAttributeRoutes(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(new ApiStackDirectRouteProvider());
        }


        public static void AddExceptionLogger(this HttpConfiguration config)
        {
            config.Services.Replace(typeof(IExceptionLogger), new ApiStackExceptionLogger());
            config.Filters.Add(new ApiStackExceptionFilterAttribute());

        }


        public static void AddMessagePack(this HttpConfiguration config)
        {
            config.Formatters.Add(new MessagePackFormatter());

        }

    }
}
