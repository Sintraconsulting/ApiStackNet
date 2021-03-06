﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.API.Model
{
  

  

    public  class WrappedResponse<T>
    {
       
        public Metadata Metadata { get; set; }

        public T Data { get; set; }

        public HttpStatusCode Status { get; set; }

        public WrappedResponse()
        {
            
        }

        public WrappedResponse(T content)
        {
            this.Data = content;
        }

    }
}

