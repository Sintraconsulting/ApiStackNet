using ApiStackNet.API.Model;
using ApiStackNet.BLL.Model;
using ApiStackNet.Core;
using ApiStackNet.DAL.Model;
using AutoMapper;
using Calabonga.PagedListLite;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace ApiStackNet.BLL
{

    public abstract class IDataService<DTO, BO, TEntityRead, TEntityWrite, PK> : IReadOnlyDataService<DTO, TEntityRead, PK>
where TEntityRead : BaseEntity<PK>
where TEntityWrite : BaseEntity<PK>
where BO : BaseEntity<PK>
where DTO : BaseEntity<PK>
    {
        public virtual int BulkSize { get => 1000; }




        public IDataService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }



        public virtual bool BulkInsert(IEnumerable<BO> entityList)
        {
            int i = 0;
            foreach (BO entity in entityList)
            {
                InternalAdd(entity);
                if (BulkSize <= i)
                {
                    i = 0;
                    dbContext.SaveChanges();
                }
            }

            return true;
        }


        public virtual bool BulkSave(IEnumerable<BO> entityList)
        {
            int i = 0;
            foreach (BO entity in entityList)
            {
                InternalSave(entity, false);
                if (BulkSize <= i)
                {
                    i = 0;
                    dbContext.SaveChanges();
                }
            }

            return true;
        }



        public virtual DTO Save(BO entity)
        {

            var internalItem = InternalSave(entity, true);

            return mapper.Map<DTO>(internalItem);
        }

        protected abstract TEntityWrite InternalGetByIdForWrite(PK Id);

        private TEntityWrite InternalSave(BO entity, bool saveChanges)
        {
            var internalItem = InternalGetByIdForWrite(entity.Id);

            if (internalItem == null)
            {
                internalItem = InternalAdd(entity);

            }
            else
            {
                internalItem = InternalUpdate(entity, internalItem);
            }

            try
            {
                if (saveChanges)
                {
                    dbContext.SaveChanges();
                }
            } catch (Exception e)
            {

            }


            return internalItem;
        }

        private TEntityWrite InternalUpdate(BO entity, TEntityWrite internalItem)
        {
            internalItem = mapper.Map<BO, TEntityWrite>(entity, internalItem);
            return internalItem;
        }

        private TEntityWrite InternalAdd(BO entity)
        {
            TEntityWrite internalItem = mapper.Map<TEntityWrite>(entity);
            //internalItem.Active = true; 

           dbContext.Set<TEntityWrite>().Add(internalItem);
            return internalItem;
        }

        public bool Delete(DTO entity)
        {
            return Delete(entity.Id);
        }

        public bool Delete(PK id)
        {
            var internalItem = InternalGetByIdForWrite(id);
            var entityToSave = dbContext.Entry<TEntityWrite>(internalItem);

            entityToSave.State = EntityState.Deleted;
            dbContext.SaveChanges();
            return true;
        }


    }
}
