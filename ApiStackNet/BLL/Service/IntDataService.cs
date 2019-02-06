using ApiStackNet.DAL.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.BLL.Service
{
    public abstract class IntDataService<DTO, BO, TEntity> : IDataService<DTO, BO, TEntity, int>
      where TEntity : AuditableEntity<int>
      where BO : BaseEntity<int>
      where DTO : BaseEntity<int>

    {
        public IntDataService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        { }

        protected override TEntity InternalGetById(int Id)
        {

            var query = GetQueriable().Where(x => x.Id == Id);

            var internalItem = query.ToList().SingleOrDefault();
            return internalItem;
        }
    }

}
