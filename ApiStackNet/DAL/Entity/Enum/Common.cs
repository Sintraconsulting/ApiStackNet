using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStackNet.DAL.Entity.Enum
{
    public enum JobStatus
    {
        PENDING,
        DOING,
        DONE,
        ERROR
    }

    public enum JobType
    {
        IMPORT,
        EXPORT,
    }

    //public enum ReportOutputType
    //{
    //    SIMPLE = 0,
    //    SEMESTRALSPLIT = 1
    //}

    public enum CurrencyType
    {
        BUDGET,
        LOCAL
    }
}
