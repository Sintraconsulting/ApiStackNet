﻿using ApiStackNet.API.Model;
using ApiStackNet.BLL;
using ApiStackNet.BLL.Model;
using ApiStackNet.BLL.Service;
using ApiStackNet.Core;
using ApiStackNet.DAL.Model;
using Calabonga.PagedListLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiStackNet.API.Controllers
{
    public class DataController<TService, DTO, TEntity, PK> : BaseController
       where TService : IReadOnlyDataService<DTO, TEntity, PK>
       where TEntity : BaseEntity<PK>
       where DTO : BaseEntity<PK>
    {
        TService Service { get; set; }

        public DataController(TService service)
        {
            this.Service = service;
        }
        [HttpGet]
        [Route("fetch")]
        public virtual WrappedResponse<DTO> Fetch(PK id)
        {
            return WrappedOK(Service.GetById(id));
        }
        [HttpPost]
        [Route("list")]
        public virtual WrappedResponse<PagedList<DTO>> List(GenericPagedFilter query)
        {
            var predicate = PredicateBuilder.False<TEntity>();

            ParameterExpression argParam = Expression.Parameter(typeof(TEntity), "x");




            var andExp = Expression.Equal(Expression.Constant(true), Expression.Constant(true));



            if (query != null && query.Filter != null)
            {
                foreach (var filter in query.Filter)
                {
                    andExp = AppendQueryClause(argParam, andExp, filter);
                }
            }

            var lambda = Expression.Lambda<Func<TEntity, bool>>(andExp, argParam);


            var orderbyList = new List<OrderInfo<TEntity>>();

            if (query != null && query.OrderBy != null)
            {
                foreach (var oder in query.OrderBy)
                {
                    orderbyList.Add(
                    new OrderInfo<TEntity>()
                    {
                        KeySelector = Expression.PropertyOrField(argParam, oder.Name),
                        Direction = oder.Direction
                    });
                }

                //orderbyList.Add(
                //    new OrderInfo<TEntity>((x) => x.Id, OrderByDirection.DESC)
                //    );
            }
            return WrappedOK(Service.List(lambda, query.PageNumber, query.PageSize, orderbyList));
        }

        private static BinaryExpression AppendQueryClause(ParameterExpression argParam, BinaryExpression andExp, Filter filter)
        {
            var nameProperty = Expression.Property(argParam, filter.Name);

            object typedValue = null;
            typedValue = ConversionHelper.StringToObject(filter.Value, nameProperty.Type);

            var value = Expression.Constant(typedValue);

            Expression clause = null;

            switch (filter.Comparator)
            {
                case QueryComparator.Equal:
                    clause = Expression.Equal(nameProperty, value);
                    break;
                case QueryComparator.Greater:
                    clause = Expression.GreaterThan(nameProperty, value);
                    break;
                case QueryComparator.Lower:
                    clause = Expression.LessThan(nameProperty, value);
                    break;
                case QueryComparator.Contains:
                    // Contains
                    clause = Expression.Equal(nameProperty, value);


                    break;
                default:
                    break;
            }

            
            andExp = Expression.AndAlso(andExp, clause);
            return andExp;
        }
    }



        public class DataController<TService,DTO, BO, TEntity, PK>: DataController<TService, DTO, TEntity, PK>
        where TService : IDataService<DTO, BO, TEntity, PK>
        where TEntity : BaseEntity<PK>
        where BO : BaseEntity<PK>
        where DTO : BaseEntity<PK>
    {
        TService Service { get; set; }

        public DataController(TService service):base(service)
        {
            this.Service = service;
        }


        
        [HttpPost]
        [Route("save")]
        public virtual WrappedResponse<DTO> Save(BO objectToSave)
        {
            return WrappedOK(Service.Save(objectToSave));
        }


        [HttpPost]
        [Route("bulk-save")]
        public virtual WrappedResponse<bool> BulkSave(IEnumerable<BO> objectToSave)
        {
            return WrappedOK(Service.BulkSave(objectToSave));
        }



        [HttpPost]
        [Route("bulk-add")]
        public virtual WrappedResponse<bool> BulkInsert(IEnumerable<BO> objectToSave)
        {
            return WrappedOK(Service.BulkInsert(objectToSave));
        }


        [HttpDelete]
        [Route("delete")]
        public virtual WrappedResponse<bool> Delete(PK Id)
        {
            return WrappedOK(Service.Delete(Id));
        }
       

    }
}
