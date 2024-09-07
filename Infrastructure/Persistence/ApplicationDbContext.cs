using Application.General.IPersistence;
using Domain.Entities._Base;
using Domain.Entities.Account;
using Domain.Entities.Admin;
using Domain.Entities.Location;
using Domain.General.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicatonUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IApplicationDBContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this._currentDateTime = DateTime.Now;
        }

        private readonly DateTime _currentDateTime;

        public Task<int> SaveChangesAsync()
        {
            foreach (var entity in ChangeTracker.Entries<IAuditableEntity>())
            {
                dynamic Data = entity.Entity;
                switch (entity.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entity.Entity.ActivityStatus = ActivityStatus.DELETED.ToString();
                        entity.Entity.ModifiedDate = _currentDateTime;
                        break;
                    case EntityState.Modified:
                        entity.Entity.ModifiedDate = _currentDateTime;
                        break;
                    case EntityState.Added:
                        Data.Id = Guid.NewGuid();
                        entity.Entity.ActivityStatus = ActivityStatus.CREATED.ToString();
                        entity.Entity.CreatedDate = _currentDateTime;
                        entity.Entity.ModifiedDate = _currentDateTime;
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync();
        }
        public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<State> States { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Pages> Pages { get; set; }

    }
}
