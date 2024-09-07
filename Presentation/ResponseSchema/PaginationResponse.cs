using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ResponseSchema
{
    public class PaginationResponse<T>
    {
        public int TotaPages { get; set; }
        public int TotalData { get; set; }
        public int CurrentPageSize { get; set; }
        public List<T> ResultList { get; set; }
    }
}
