using Domain.Entities._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Location
{
    public class State : AuditableWithBaseEntity<Guid>
    {
        public string StateName { get; set; } = string.Empty;
        public bool IsActive { get; set; }



        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
