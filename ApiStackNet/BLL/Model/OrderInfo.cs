using ApiStackNet.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.BLL.Model
{
    public class OrderInfo<TEntity>
    {
      public MemberExpression KeySelector { get; set; }

      public OrderByDirection Direction { get; set; }

        public OrderInfo()
        { }

        public OrderInfo(Expression<Func<TEntity, object>> expr, OrderByDirection dir)
        {
            this.Direction = dir;
            var body=expr.Body;

            MemberExpression me;
            switch (expr.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expr.Body as UnaryExpression;
                    me = ((ue != null) ? ue.Operand : null) as MemberExpression;
                    break;
                default:
                    me = expr.Body as MemberExpression;
                    break;
            }

            KeySelector = me;
        }
    }
}
