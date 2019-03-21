using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.DAL.Model
{
    public class AuditableEntity<PKType>:BaseEntity<PKType>
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }



        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }


        public bool Active { get; set; }

    }
}

