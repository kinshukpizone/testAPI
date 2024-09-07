using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ResponseSchema
{
    public class RequestPaginationModel
    {
        public int PageNumber { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
