using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DataTransferObject.Admin
{
    public class PageModel
    {
        public string Name { get; set; }
        public ICollection<PermissionModel> PermissionNavigation { get; set; }
    }
}
