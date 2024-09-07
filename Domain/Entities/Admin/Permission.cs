using Domain.Entities._Base;
using Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Admin
{
    public class Permission : AuditableWithBaseEntity<Guid>
    {
        public Guid UserID { get; set; }
        public Guid PageId { get; set; }
        [ForeignKey(nameof(UserID))]
        public ApplicationUser UserNavigation { get; set; }
        [ForeignKey(nameof(PageId))]
        public Pages PageNavigation { get; set; }
    }
}
