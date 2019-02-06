using ApiStackNet.API.Model;
using ApiStackNet.BLL;
using ApiStackNet.BLL.Service;
using ApiStackNet.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiStackNet.API.Controllers
{
    public class DataController<TService,DTO, BO, TEntity, PK>: BaseController
        where TService : IDataService<DTO, BO, TEntity, PK>
        where TEntity : AuditableEntity<PK>
        where BO : BaseEntity<PK>
        where DTO : BaseEntity<PK>
    {
        TService Service { get; set; }

        public DataController(TService service)
        {
            this.Service = service;
        }

        public WrappedResponse<DTO> Get(PK id)
        {
            return WrappedOK(Service.GetById(id));
        }

    }
}
