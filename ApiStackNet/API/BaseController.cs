using ApiStackNet.API.Model;
using ApiStackNet.BLL.Service;
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
        public MessageService MessageService { get; set; }

       
     
        protected   WrappedResponse<T> WrappedOK<T>(T o)
        {
            var result = new WrappedResponse<T>(o);
            
            // UIMESSAGE MANAGING:

            // WARNING:: To avoid a null pointer exception
            // if service are not injected, check if 
            // MessageService is not null
            
            if (MessageService!=null && MessageService.HasMessages)
            {

                if (result.Metadata == null)
                {
                    result.Metadata = new Metadata();
                }

                result.Metadata.UiMessages = MessageService.UiMessages;

            }

            return result;
        }
    }
}
