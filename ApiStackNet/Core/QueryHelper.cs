using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.Core
{
    public static class QueryHelper
    {
        private static readonly MethodInfo OrderByMethod =
            typeof(Queryable).GetMethods().Single(method =>
           method.Name == "OrderBy" && method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod =
            typeof(Queryable).GetMethods().Single(method =>
           method.Name == "OrderByDescending" && method.GetParameters().Length == 2);

        private static readonly MethodInfo ThenOrderByMethod =
          typeof(Queryable).GetMethods().Single(method =>
         method.Name == "ThenBy" && method.GetParameters().Length == 2);

        private static readonly MethodInfo ThenOrderByDescendingMethod =
            typeof(Queryable).GetMethods().Single(method =>
           method.Name == "ThenByDescending" && method.GetParameters().Length == 2);

        public static bool PropertyExists<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) != null;
            
        }

        public static IQueryable<T> OrderByProperty<T>(
           this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            return OrderByGeneric(OrderByMethod, source, propertyName);
        }


        public static IQueryable<T> ThenOrderByProperty<T>(
           this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            return OrderByGeneric(ThenOrderByMethod, source, propertyName);
        }


        public static IQueryable<T> OrderByPropertyDescending<T>(
           this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            return OrderByGeneric(OrderByDescendingMethod, source, propertyName);
        }


        public static IQueryable<T> ThenOrderByPropertyDescending<T>(
           this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            return OrderByGeneric(ThenOrderByDescendingMethod, source, propertyName);
        }
        private static IQueryable<T> OrderByGeneric<T>(MethodInfo info,IQueryable<T> source, string propertyName)
        {
            ParameterExpression paramterExpression = Expression.Parameter(typeof(T));
            Expression orderByProperty = Expression.Property(paramterExpression, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByProperty, paramterExpression);
            MethodInfo genericMethod =
              info.MakeGenericMethod(typeof(T), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }

       
    }
}
