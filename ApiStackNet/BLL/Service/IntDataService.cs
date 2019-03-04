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


    public abstract class IntDataService<DTO, BO, TEntity, TEntityWrite> : IDataService<DTO, BO, TEntity, TEntityWrite, int>
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


    public abstract class IntDataService<DTO, BO, TEntity> : IDataService<DTO, BO, TEntity, TEntity, int>
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

        protected override TEntity InternalGetByIdForWrite(int Id)
        {
            return InternalGetById();

        }
    }



    public abstract class IntDataService<DTO,  TEntity> : IReadOnlyDataService<DTO,  TEntity, int>
     where TEntity : BaseEntity<int>
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
