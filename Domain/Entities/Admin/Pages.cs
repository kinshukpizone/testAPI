using Domain.Entities._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Admin
{
    public class Pages: AuditableWithBaseEntity<Guid>
    {
        public string Name { get; set; }
        public ICollection<Permission> PermissionNavigation { get; set; }
    }
}
