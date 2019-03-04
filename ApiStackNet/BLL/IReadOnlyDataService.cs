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
    public abstract class IReadOnlyDataService<DTO, TEntityRead, PK>
    where TEntityRead : BaseEntity<PK>
    where DTO : BaseEntity<PK>
    {

        protected DbContext dbContext;
        protected IMapper mapper;

        public virtual IQueryable<TEntityRead> GetQueriable()
        {
            var query = GetSet().AsQueryable();

            if ((typeof(TEntityRead)).BaseType.Name == "AuditableEntity`1")//TODO: make generic
            {
                //   query = query.Where(x => x.Active == true);

                ParameterExpression paramterExpression = Expression.Parameter(typeof(TEntityRead));
                var idProperty = Expression.Property(paramterExpression, "Active");
                var comparison= Expression.Equal(idProperty, Expression.Constant(true));
                var lambda = Expression.Lambda<Func<TEntityRead, bool>>(comparison, paramterExpression);
                query = Queryable.Where(query, lambda);
            }
            return query;
        }

        public virtual  DbSet<TEntityRead> GetSet()
        {
            return dbContext.Set<TEntityRead>();
        }


        public virtual DTO GetById(PK Id)
        {
            TEntityRead internalItem = InternalGetById(Id);
            return mapper.Map<DTO>(internalItem);
        }

        protected abstract TEntityRead InternalGetById(PK Id);

        public IReadOnlyDataService(DbContext dbContext, IMapper mapper)
        {
            //TODO: move using DI
            this.dbContext = dbContext;
            this.mapper = mapper;

        }



        public virtual PagedList<DTO> List(int page, int pageSize)
        {
            return List(null, page, pageSize, null);
        }

        public virtual PagedList<DTO> List(Expression<Func<TEntityRead, bool>> where, int page, int pageSize)
        {
            return List(where, page, pageSize, null);
        }



        public virtual PagedList<DTO> List(Expression<Func<TEntityRead, bool>> where, int page, int pageSize, IList<OrderInfo<TEntityRead>> orderbyList)
        {
            var query = GetQueriable();
            if (where != null)
            {
                query = query.Where(where);
            }

            int count = query.Count();

            //TODO: add sort and default values
            if (orderbyList != null && orderbyList.Count > 0)
            {
                int i = 0;
                foreach (var ob in orderbyList)
                {
                    query = SetOrderByClause(query, i, ob);
                    //query = query.OrderBy(lambda);

                    i++;
                }
            }
            else
            {
                //paging require almost 1 sort dimension
                query = query.OrderBy(x => x.Id);
            }


            if (page > 0)
            {
                query = query.Skip(page * pageSize);
            }
            if (pageSize > 0)
            {
                query = query.Take(pageSize);
            }


            var dtos = mapper.Map<List<DTO>>(query.ToList());

            var list = new PagedList<DTO>(dtos, page, pageSize, count);

            return list;
        }

        private static IQueryable<TEntityRead> SetOrderByClause(IQueryable<TEntityRead> query, int i, OrderInfo<TEntityRead> ob)
        {

            if (i == 0)
            {
                if (ob.Direction == OrderByDirection.ASC)
                {
                    return QueryHelper.OrderByProperty<TEntityRead>(query, ob.KeySelector.Member.Name);
                }
                else
                {
                    return QueryHelper.OrderByPropertyDescending<TEntityRead>(query, ob.KeySelector.Member.Name);
                }
            }
            else
            {
                if (ob.Direction == OrderByDirection.ASC)
                {
                    return QueryHelper.ThenOrderByProperty<TEntityRead>(query, ob.KeySelector.Member.Name);
                }
                else
                {
                    return QueryHelper.ThenOrderByPropertyDescending<TEntityRead>(query, ob.KeySelector.Member.Name);
                }
            }


        }
    }


}
