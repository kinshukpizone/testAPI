using Domain.Entities._Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Account
{
    public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? ActivityStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid EditBy { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
