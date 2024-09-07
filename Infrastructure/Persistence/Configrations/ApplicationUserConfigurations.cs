using Domain.Entities.Account;
using Domain.General.Enums;
using Domain.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configrations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(nameof(ApplicationUser));

            builder.HasData(
                new ApplicationUser()
                {
                    Id = ApplicationUserSeedData.Id,
                    Email = ApplicationUserSeedData.Email,
                    NormalizedEmail = ApplicationUserSeedData.Email.ToUpper(),
                    UserName = ApplicationUserSeedData.Username,
                    NormalizedUserName = ApplicationUserSeedData.Username.ToUpper(),
                    PasswordHash = ApplicationUserSeedData.Password,
                    FirstName = ApplicationUserSeedData.FirstName,
                    LastName = ApplicationUserSeedData.LastName,
                    ActivityStatus = ActivityStatus.CREATED.ToString(),
                    EmailConfirmed = true,
                    ConcurrencyStamp = ApplicationUserSeedData.ConcurrencyStamp.ToString(),
                });
        }
    }
}
