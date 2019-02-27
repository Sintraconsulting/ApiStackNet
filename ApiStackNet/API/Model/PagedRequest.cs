using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.API.Model
{
    public class PagedRequest<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 20;

        public T Filter { get; set; }


        public IEnumerable<OrderBy> OrderBy{get;set;} = new List<OrderBy>();
    }
}
