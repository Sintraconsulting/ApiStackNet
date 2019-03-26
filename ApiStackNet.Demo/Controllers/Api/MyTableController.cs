using ApiStackNet.API.Controllers;
using ApiStackNet.API.Model;
using ApiStackNet.Demo.BLL.Services;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiStackNet.Demo.Controllers.Api
{
    [RoutePrefix("api/mytable")]
    public class MyTableController : DataController<MyTableDataService, MyTableDTO, MyTableBO, MyTable, int>
    {
        private MyTableDataService MyTableDataService;
      
        public MyTableController(MyTableDataService service) : base(service)
        {
            this.MyTableDataService = service;
        }

        [HttpGet]
        [Route("{id}")]
        public WrappedResponse<MyTableDTO> GetById([FromUri] int id)
        {
            return WrappedOK(this.MyTableDataService.GetById(id));
        }
    }
}