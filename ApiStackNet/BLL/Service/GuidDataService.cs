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
    public abstract class GuidDataService<DTO, BO, TEntity> : IDataService<DTO, BO, TEntity, Guid>
      where TEntity : BaseEntity<Guid>
      where BO : BaseEntity<Guid>
      where DTO : BaseEntity<Guid>

    {
        public GuidDataService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        { }

        protected override TEntity InternalGetById(Guid Id)
        {
            var query = GetQueriable().Where(x => x.Id == Id);
            var internalItem = query.ToList().SingleOrDefault();
            return internalItem;
        }
    }


    public abstract class ReadOblyGuidDataService<DTO,  TEntity> : IReadOnlyDataService<DTO, TEntity, Guid>
     where TEntity : BaseEntity<Guid>
     where DTO : BaseEntity<Guid>

    {
        public ReadOblyGuidDataService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        { }

        protected override TEntity InternalGetById(Guid Id)
        {
            var query = GetQueriable().Where(x => x.Id == Id);
            var internalItem = query.ToList().SingleOrDefault();
            return internalItem;
        }
    }

}
