using Domain.Entities.Account;
using Domain.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configrations
{
    public class ApplicationUserRoleConfigurations : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable(nameof(ApplicationUserRole));
            builder.HasData(
                new ApplicationUserRole()
                {
                    RoleId = ApplicationRoleSeedData._idSuperAdmin,
                    UserId = ApplicationUserSeedData.Id
                });
        }
    }
}
