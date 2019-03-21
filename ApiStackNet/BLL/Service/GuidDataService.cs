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


  public abstract class GuidDataService<DTO, BO, TEntity, TEntityWrite> : IDataService<DTO, BO, TEntity, TEntityWrite, Guid>
  where TEntity : BaseEntity<Guid>
  where TEntityWrite : BaseEntity<Guid>
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

        protected override TEntityWrite InternalGetByIdForWrite(Guid Id)
        {
            var query = this.dbContext.Set<TEntityWrite>().AsQueryable().Where(x => x.Id == Id);
            var internalItem = query.ToList().SingleOrDefault();
            return internalItem;
        }
    }

    public abstract class GuidDataService<DTO, BO, TEntity> : IDataService<DTO, BO, TEntity, TEntity, Guid>
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

        protected override TEntity InternalGetByIdForWrite(Guid Id)
        {
            return InternalGetById( Id);
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
