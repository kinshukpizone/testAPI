using Domain.Entities.Account;
using Domain.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configrations
{
    public class ApplicationRoleConfigurations : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable(nameof(ApplicationRole));
            builder.HasData(
                new ApplicationRole()
                {
                    Id = ApplicationRoleSeedData._idSuperAdmin,
                    Name = AppRoles.SUPERADMIN,
                    NormalizedName = AppRoles.SUPERADMIN.ToString().ToUpper(),
                    ConcurrencyStamp = null
                });
        }
    }
}
