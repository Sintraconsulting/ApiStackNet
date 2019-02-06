﻿using ApiStackNet.DAL.Model;
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



    public abstract class IDataService<DTO, BO, TEntity, PK>
    where TEntity : AuditableEntity<PK>
    where BO : BaseEntity<PK>
    where DTO : BaseEntity<PK>

    {
        public virtual int BulkSize { get => 1000; }
       

        private DbContext dbContext;
        private IMapper mapper;

        
        public  IDataService(DbContext dbContext, IMapper mapper)
        {
            //TODO: move using DI
            this.dbContext = dbContext;
            this.mapper = mapper;
          
        }

        public IQueryable<TEntity> GetQueriable()
        {
            var query= GetSet().AsQueryable();

            if ((typeof(TEntity)).BaseType.Name == "AuditableEntity`1")//TODO: make generic
            {
                query = query.Where(x => x.Active == true);
            }
            return query;
        }

        public DbSet<TEntity> GetSet()
        {
            return dbContext.Set<TEntity>();
        }


        public virtual DTO GetById(PK Id)
        {
            TEntity internalItem = InternalGetById(Id);
            return mapper.Map<DTO>(internalItem);
        }

        protected abstract TEntity InternalGetById(PK Id);

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


        public virtual bool BulkUpdate(IEnumerable<BO> entityList)
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

          var  internalItem = InternalSave(entity, true);

            return mapper.Map<DTO>(internalItem);
        }

        private TEntity InternalSave(BO entity, bool saveChanges)
        {
            var internalItem = InternalGetById(entity.Id);

            if (internalItem == null)
            {
                internalItem = InternalAdd(entity);

            }
            else
            {
                internalItem = InternalUpdate(entity, internalItem);
            }

            if (saveChanges)
            {
                dbContext.SaveChanges();
            }

            return internalItem;
        }

        private TEntity InternalUpdate(BO entity, TEntity internalItem)
        {
            internalItem = mapper.Map<BO, TEntity>(entity, internalItem);
            return internalItem;
        }

        private TEntity InternalAdd(BO entity)
        {
            TEntity internalItem = mapper.Map<TEntity>(entity);
            internalItem.Active = true;

            GetSet().Add(internalItem);
            return internalItem;
        }

        public  bool Delete(DTO entity)
        {
            return Delete(entity.Id);
        }

        public bool Delete(PK id)
        {
            var internalItem = InternalGetById(id);
            var entityToSave = dbContext.Entry<TEntity>(internalItem);

            entityToSave.State = EntityState.Deleted;
            dbContext.SaveChanges();
            return true;
        }



        public virtual PagedList<DTO> List(Expression<Func<TEntity,bool>> where, int page,int pageSize)
        {
            var query = GetQueriable().Where(where);
          

                int count = query.Count();
            if (page > 0)
            {
                query = query.Skip(page * pageSize);
            }
            if (pageSize > 0)
            {
                query = query.Take(pageSize);
            }

            var dtos= mapper.Map<List<DTO>>(query.ToList());

            var list = new PagedList<DTO>(dtos, page, pageSize, count);

            return list;
        }


    }
}