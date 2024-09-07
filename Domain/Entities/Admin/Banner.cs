using Domain.Entities._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Admin
{
    public class Banner : AuditableWithBaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public bool isActive { get; set; }

    }
}
