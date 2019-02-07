using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.DAL.Model
{
    public class BaseEntity<PKType>
    {
        public virtual PKType Id { get; set; }
    }
}
