using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiStackNet.API.Config
{
    public static class WebApiExtension
    {
        public static void ApiStackMapHttpAttributeRoutes(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(new ApiStackDirectRouteProvider());
        }

       
    }
}
