using ApiStackNet.API.Model;
using ApiStackNet.BLL;
using ApiStackNet.BLL.Model;
using ApiStackNet.BLL.Service;
using ApiStackNet.Core;
using ApiStackNet.DAL.Model;
using Calabonga.PagedListLite;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiStackNet.API.Controllers
{
    public class ReadOnlyDataController<TService, DTO, TEntity, PK> : BaseController
       where TService : IReadOnlyDataService<DTO, TEntity, PK>
       where TEntity : BaseEntity<PK>
       where DTO : BaseEntity<PK>
    {
        TService Service { get; set; }

        public ReadOnlyDataController(TService service) : base()
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


            Filter normalized = new Filter();
            normalized.Inner = query.Filter;

            Expression andExp = default(Expression);
            if (query != null && query.Filter != null && query.Filter.Count > 0)
            {
                andExp = RecursiveAddFilter(normalized, argParam, andExp);
            } else
            {
                andExp = Expression.Equal(Expression.Constant(true), Expression.Constant(true));
            }

            var lambda = Expression.Lambda<Func<TEntity, bool>>(andExp, argParam);


            var orderbyList = new List<OrderInfo<TEntity>>();

            if (query != null && query.OrderBy != null)
            {
                foreach (var order in query.OrderBy)
                {
                    // check if order is valid
                    MemberExpression nameProperty = null;
                    try
                    {
                        nameProperty = Expression.Property(argParam, order.Name);
                        orderbyList.Add(
                           new OrderInfo<TEntity>()
                           {
                               KeySelector = nameProperty,
                               Direction = order.Direction
                           }
                       );
                    }
                    catch (Exception e)
                    {
                        //logger.Warn($"Order error: field not valid: {e.Message}");
                        MessageService.AddWarning($"Order error: field not valid: {e.Message}", UiMessageTarget.TOAST, "Filter Parser", "SFW_102", order.Name);

                    }


                }

                //orderbyList.Add(
                //    new OrderInfo<TEntity>((x) => x.Id, OrderByDirection.DESC)
                //    );
            }
            return WrappedOK(Service.List(lambda, query.PageNumber, query.PageSize, orderbyList));
        }

        private Expression RecursiveAddFilter(Filter filter, ParameterExpression argParam, Expression parentExpression)
        {

            Expression exprToAppend = null;

            if (filter.Inner != null && filter.Inner.Count > 0)
            {

                //Resolve as expression node
                foreach (var inner in filter.Inner)
                {
                    var nodeExpr = RecursiveAddFilter(inner, argParam, exprToAppend);
                    if (nodeExpr == null)
                    {
                        continue;
                    }

                    if (exprToAppend == null)
                    {
                        exprToAppend = nodeExpr;
                    }
                    else
                    {
                        if (inner.Conjunction == Conjunction.AND)
                        {
                            exprToAppend = Expression.AndAlso(exprToAppend, nodeExpr);
                        }
                        else
                        {
                            exprToAppend = Expression.OrElse(exprToAppend, nodeExpr);
                        }
                    }
                    
                }


            }
            else
            {
                exprToAppend = GetQueryClause(argParam, filter);
            }

            return exprToAppend;
        }

        private Expression  GetQueryClause(ParameterExpression argParam,  Filter filter)
        {
            MemberExpression nameProperty = null;

            try
            {
                nameProperty = Expression.Property(argParam, filter.Name);

            }
            catch (Exception e)
            {
                //logger.Warn($"Filter error: field not valid: {e.Message}");
                MessageService.AddWarning($"Filter error: field not valid: {e.Message}", UiMessageTarget.TOAST, "Filter Parser", "SFW_101", filter.Name);
                return null;
            }


            object typedValue = null;

            var value = Expression.Constant("");

            if (filter.Comparator != QueryComparator.In)
            {
                typedValue = ConversionHelper.StringToObject(filter.Value, nameProperty.Type);

                value = Expression.Constant(typedValue, nameProperty.Type);
            }

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
                    var propertyExp = Expression.Property(argParam, filter.Name);
                    MethodInfo method = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                    //var constant = Expression.Constant(value.Value, typeof(string));
                    clause = Expression.Call(propertyExp, method, Expression.Constant(value.Value));
                    break;
                case QueryComparator.In:
                    // Contains
                    var propertyExp2 = Expression.Property(argParam, filter.Name);

                    string filterValues = filter.Value;

                    if (!filterValues.StartsWith("["))
                    {
                        filterValues = String.Concat("[", filterValues);
                    }

                    if (!filterValues.EndsWith("]"))
                    {
                        filterValues = String.Concat(filterValues, "]");
                    }

                    var values = JArray.Parse(filterValues);

                    var listType = typeof(List<>).MakeGenericType(nameProperty.Type);
                    var listInstance = Activator.CreateInstance(listType);

                    var addmethod = listType.GetMethod("Add");
                    foreach (var val in values)
                    {
                        var typedVal = val.ToObject(nameProperty.Type);
                        addmethod.Invoke(listInstance, new object[] { typedVal });
                    }

                    MethodInfo method2 = listType.GetMethod("Contains", new Type[] { nameProperty.Type });
                    clause = Expression.Call(Expression.Constant(listInstance), method2, propertyExp2);

                    break;
                default:
                    break;
            }

            return clause;
        }
    }

    public class DataController<TService, DTO, BO, TEntity,TEntityWrite, PK> : ReadOnlyDataController<TService, DTO, TEntity, PK>
     where TService : IDataService<DTO, BO, TEntity, TEntityWrite, PK>
     where TEntity : BaseEntity<PK>
     where TEntityWrite : BaseEntity<PK>
     where BO : BaseEntity<PK>
     where DTO : BaseEntity<PK>
    {
        TService Service { get; set; }

        public DataController(TService service) : base(service)
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

    public class DataController<TService, DTO, BO, TEntity, PK> : ReadOnlyDataController<TService, DTO, TEntity, PK>
    where TService : IDataService<DTO, BO, TEntity, TEntity, PK>
    where TEntity : BaseEntity<PK>
    where BO : BaseEntity<PK>
    where DTO : BaseEntity<PK>
    {
        TService Service { get; set; }

        public DataController(TService service) : base(service)
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
