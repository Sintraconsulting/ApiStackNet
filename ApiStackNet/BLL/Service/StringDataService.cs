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

    public abstract class StringDataService<DTO, BO, TEntity, TEntityWrite> : IDataService<DTO, BO, TEntity, TEntityWrite, string>
    where TEntity : BaseEntity<string>
    where TEntityWrite : BaseEntity<string>
    where BO : BaseEntity<string>
    where DTO : BaseEntity<string>

    {
        public StringDataService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        { }

        protected override TEntity InternalGetById(string Id)
        {
            var query = GetQueriable().Where(x => x.Id == Id);
            var internalItem = query.ToList().SingleOrDefault();
            return internalItem;
        }

        protected override TEntityWrite InternalGetByIdForWrite(string Id)
        {
            var query = this.dbContext.Set<TEntityWrite>().AsQueryable().Where(x => x.Id == Id);
            var internalItem = query.ToList().SingleOrDefault();
            return internalItem;
        }
    }


    public abstract class StringDataService<DTO, BO, TEntity> : IDataService<DTO, BO, TEntity, TEntity, string>
      where TEntity : AuditableEntity<string>
      where BO : BaseEntity<string>
      where DTO : BaseEntity<string>
    {
        public StringDataService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        { }

        protected override TEntity InternalGetById(string Id)
        {
            var query = GetQueriable().Where(x => x.Id == Id);
            var internalItem = query.ToList().SingleOrDefault();
            return internalItem;
        }
    }


    public abstract class StringDataService<DTO,  TEntity> : IReadOnlyDataService<DTO,  TEntity, string>
     where TEntity : BaseEntity<string>
     where DTO : BaseEntity<string>
    {
        public StringDataService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        { }

        protected override TEntity InternalGetById(string Id)
        {
            var query = GetQueriable().Where(x => x.Id == Id);
            var internalItem = query.ToList().SingleOrDefault();
            return internalItem;
        }
    }

}
