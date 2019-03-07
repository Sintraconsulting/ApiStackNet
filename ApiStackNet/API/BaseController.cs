using ApiStackNet.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiStackNet.API.Controllers
{
    public class BaseController:ApiController
    {
        protected WrappedResponse<T> WrappedOK<T>(T o)
        {
            var result = new WrappedResponse<T>(o);

            return result;
        }
    }
}
