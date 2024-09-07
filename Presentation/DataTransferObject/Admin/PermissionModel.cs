using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DataTransferObject.Admin
{
    public class PermissionModel
    {
        public Guid UserID { get; set; }
        public Guid PageId { get; set; }
        public RegisterUserModel UserNavigation { get; set; }
        public PageModel PageNavigation { get; set; }
    }
}
