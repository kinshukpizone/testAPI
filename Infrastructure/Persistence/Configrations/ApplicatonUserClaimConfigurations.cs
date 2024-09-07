using Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configrations
{
    public class ApplicatonUserClaimConfigurations : IEntityTypeConfiguration<ApplicatonUserClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicatonUserClaim> builder)
        {
            builder.ToTable(nameof(ApplicatonUserClaim));
        }
    }
}
