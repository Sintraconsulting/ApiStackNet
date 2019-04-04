using ApiStackNet.BLL.Service;
using ApiStackNet.Demo.BO;
using ApiStackNet.Demo.DTO;
using ApiStackNet.Demo.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiStackNet.Demo.BLL.Services
{
    public class MyTableDataService : IntDataService<MyTableDTO, MyTableBO, MyTable>
    {
        public MyTableDataService(DbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public MyTableDTO SaveMyTable(MyTableBO myTableBO)
        {
            MyTableDTO myTableDTO = base.Save(myTableBO);
            
            return myTableDTO;
        }
    }
}