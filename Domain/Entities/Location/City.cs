using Domain.Entities._Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Location
{
    public class City : AuditableWithBaseEntity<Guid>
    {
        public string CityName { get; set; } = string.Empty;

        public Guid? StateId { get; set; }
        public bool IsActive { get; set; }

        #region Navigation
        [ForeignKey(nameof(StateId))]
        public State? StateNavigation { get; set; }
        #endregion
    }
}
