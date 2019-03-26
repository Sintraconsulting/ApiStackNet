using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.DAL.Model
{
    public class AuditableEntity<PKType>:BaseEntity<PKType>
    {
        public virtual DateTime CreatedOn { get; set; }
        public virtual DateTime ModifiedOn { get; set; }
        public virtual DateTime DeletedOn { get; set; }



        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual string DeletedBy { get; set; }


        public virtual bool Active { get; set; }

    }
}

