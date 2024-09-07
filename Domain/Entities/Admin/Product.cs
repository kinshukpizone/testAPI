using Domain.Entities._Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Admin
{
    public class Product : AuditableWithBaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
        public string ImageUrl  { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public  Category CategoryNavigation { get; set; }
    }
}
