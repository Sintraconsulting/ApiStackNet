using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace ApiStackNet.API
{

    public class ApiStackExceptionFilterAttribute:ExceptionFilterAttribute
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            logger.Info(actionExecutedContext.Request);
            logger.Fatal(actionExecutedContext.Exception);
            if (actionExecutedContext.Exception is TypeLoadException)
            {
                logger.Info("Unable to load" + ((TypeLoadException)actionExecutedContext.Exception).TypeName);
            }          
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An unhandled exception was thrown by service."),  
                ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
            };
            actionExecutedContext.Response = response;
        }
    }

    public class ApiStackExceptionLogger : ExceptionLogger
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        public override void Log(ExceptionLoggerContext context)
        {
            logger.Info(context.Request);
            logger.Fatal(context.Exception);
            if (context.Exception is TypeLoadException)
            {
                logger.Info("Unable to load" + ((TypeLoadException)context.Exception).TypeName);               
            }
            base.Log(context);
        }
    }
}
