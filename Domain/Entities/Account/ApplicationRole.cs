using Domain.Entities._Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Account
{
    public class ApplicationRole : IdentityRole<Guid>, IAuditableEntity
    {
        public string? ActivityStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid EditBy { get; set; }
        public Guid CreatedBy { get; set; }

    }
}
