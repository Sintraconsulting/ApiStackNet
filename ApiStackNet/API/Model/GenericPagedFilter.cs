using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.API.Model
{
    public enum OrderByDirection
    {
        ASC,
        DESC
    }
    public enum QueryComparator
    {
        Equal,
        Greater,
        Lower,
        Contains
    }

    public class Filter
    {    
       public string Name { get; set; }
       public  QueryComparator Comparator { get;set; }
       public string Value { get; set; }
    }

    public class OrderBy
    {
        public string Name { get; set; }
        public OrderByDirection Direction { get; set; }
    }

        public class GenericPagedFilter: PagedRequest<List<Filter>>
    {
        public GenericPagedFilter()
        {
            this.Filter = new List<Filter>();
        }
    }
}
